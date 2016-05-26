using MassTransit;
using MassTransit.RabbitMQ.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masstransit.RabbitMQ.SubscriberA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SubscriberA");
            var bus = BusCreator.CreateBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, RabbitMqConstants.GreetingEventSubscriberAQueue, e =>
                {
                    e.Consumer<GreetingEventConsumer>();
                });
            });

            bus.Start();

            Console.WriteLine("Listening for Greeting events.. Press enter to exit");
            Console.ReadLine();

            bus.Stop();
        }
    }
}
