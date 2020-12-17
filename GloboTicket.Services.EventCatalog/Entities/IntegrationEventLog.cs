using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboTicket.Services.EventCatalog.Entities
{
    public class IntegrationEventLog
    {
        public int IntegrationEventLogId { get; set; }
        public string IntegrationEventType { get; set; }
        public string ServiceBusTopicName { get; set; }
        public string IntegrationEventBody { get; set; }
        public string State { get; set; }
    }
}
