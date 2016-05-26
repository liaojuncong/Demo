using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masstransit.RabbitMQ.Message
{
    public class GreetingCommand
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
    }
}
