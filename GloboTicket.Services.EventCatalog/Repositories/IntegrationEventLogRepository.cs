using GloboTicket.Services.EventCatalog.DbContexts;
using GloboTicket.Services.EventCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboTicket.Services.EventCatalog.Repositories
{
    public class IntegrationEventLogRepository : IIntegrationEventLogRepository
    {
        private readonly EventCatalogDbContext _dbContext;

        public IntegrationEventLogRepository(EventCatalogDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void AddEventLogEntry(IntegrationEventLog logEntry)
        {
            _dbContext.IntegrationEventLogs.Add(logEntry);
        }
    }
}
