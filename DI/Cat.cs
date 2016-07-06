using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    public class Cat
    {
        private ConcurrentDictionary<Type, Type> typeMapping = new ConcurrentDictionary<Type, Type>();

        public void Register<T1, T2>()
        {
            Register(typeof(T1), typeof(T2));
        }
        public void Register(Type from, Type to)
        {
            typeMapping[from] = to;
        }
        public T GetService<T>() where T : class
        {
            return this.GetService(typeof(T)) as T;
        }
        public object GetService(Type serviceType)
        {
            Type type;
            if (!typeMapping.TryGetValue(serviceType, out type))
            {
                type = serviceType;
            }
            if (type.IsInterface || type.IsAbstract)
            {
                return null;
            }
            ConstructorInfo constructor = this.GetConstructor(type);
            if (null == constructor)
            {
                return null;
            }

            object[] arguments = constructor.GetParameters().Select(p => this.GetService(p.ParameterType)).ToArray();
            object service = constructor.Invoke(arguments);
            this.InitializeInjectedProperties(service);
            this.InvokeInjectedMethods(service);
            return service;
        }

        protected virtual ConstructorInfo GetConstructor(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();
            return constructors.FirstOrDefault(c => c.GetCustomAttribute<InjectionAttribute>() != null) ?? constructors.FirstOrDefault();
        }

        protected virtual void InitializeInjectedProperties(object service)
        {
            PropertyInfo[] properties = service.GetType().GetProperties()
                .Where(p => p.CanWrite && p.GetCustomAttribute<InjectionAttribute>() != null)
                .ToArray();
            Array.ForEach(properties, p => p.SetValue(service, this.GetService(p.PropertyType)));
        }

        protected virtual void InvokeInjectedMethods(object service)
        {
            MethodInfo[] methods = service.GetType().GetMethods()
                .Where(m => m.GetCustomAttribute<InjectionAttribute>() != null)
                .ToArray();
            Array.ForEach(methods, m =>
            {
                object[] arguments = m.GetParameters().Select(p => this.GetService(p.ParameterType)).ToArray();
                m.Invoke(service, arguments);
            });
        }
    }
}
