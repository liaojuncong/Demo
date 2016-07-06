using Masstransit.RabbitMQ.Message;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masstransit.RabbitMQ.ReceiveClient
{
    public class GreetingConsumer : IConsumer<GreetingCommand>
    {
        public async Task Consume(ConsumeContext<GreetingCommand> context)
        {

            await Console.Out.WriteLineAsync($"receive greeting commmand: {context.Message.Id},{context.Message.DateTime}");

            //var greetingEvent = new GreetingEvent
            //{
            //    Id = context.Message.Id,
            //    DateTime = DateTime.Now
            //};

            //await context.Publish(greetingEvent);
        }
    }
}
