using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masstransit.RabbitMQ.Message
{
    public class GreetingEvent
    {
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }
    }
}
