using GloboTicket.Services.EventCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboTicket.Services.EventCatalog.Repositories
{
    public interface IIntegrationEventLogRepository
    {
        void AddEventLogEntry(IntegrationEventLog logEntry);
    }
}
