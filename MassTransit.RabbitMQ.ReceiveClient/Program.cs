using MassTransit;
using MassTransit.RabbitMQ.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masstransit.RabbitMQ.ReceiveClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "RabbitMQ.ReceiveClient";

            var bus = BusCreator.CreateBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, RabbitMqConstants.GreetingQueue, e =>
                {
                    e.Consumer<GreetingConsumer>();
                });
            });

            bus.Start();

            Console.WriteLine("Listening for Greeting commands.. Press enter to exit");
            Console.ReadLine();

            bus.Stop();
        }
    }
}
