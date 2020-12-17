using System;
using GloboTicket.Integration.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboTicket.Services.EventCatalog.Messages
{
    public class EventUpdatedMessage : IntegrationBaseMessage
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Price { get; set; }
        public string Message { get; set; }
    }
}
