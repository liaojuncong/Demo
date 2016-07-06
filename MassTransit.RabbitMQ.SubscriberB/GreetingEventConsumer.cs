using Masstransit.RabbitMQ.Message;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masstransit.RabbitMQ.SubscriberB
{
    public class GreetingEventConsumer : IConsumer<GreetingEvent>
    {
        public async Task Consume(ConsumeContext<GreetingEvent> context)
        {
            await Console.Out.WriteLineAsync($"receive greeting event: id {context.Message.Id}");
        }
    }
}
