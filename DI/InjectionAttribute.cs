using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    [AttributeUsage(AttributeTargets.Constructor |
                     AttributeTargets.Property |
                     AttributeTargets.Method,
                     AllowMultiple = false)]
    public class InjectionAttribute : Attribute { }
}
