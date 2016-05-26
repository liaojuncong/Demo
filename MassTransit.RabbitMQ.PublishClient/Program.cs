using Masstransit.RabbitMQ.Message;
using MassTransit.RabbitMQ.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransit.RabbitMQ.PublishClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press 'Enter' to send a message.To exit, Ctrl + C");

            var bus = BusCreator.CreateBus();

            while (Console.ReadLine() != null)
            {
                Task.Run(() => SendCommand(bus)).Wait();
            }

            Console.ReadLine();
        }

        private static async void SendCommand(IBusControl bus)
        {
            var command = new GreetingEvent()
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.Now
            };

            await bus.Publish<GreetingEvent>(command);

            Console.WriteLine($"send command:id={command.Id},{command.DateTime}");
        }
    }
}
