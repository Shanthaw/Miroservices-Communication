using Globoticket.Services.IntegrationEventPublisher.DbContexts;
using Globoticket.Services.IntegrationEventPublisher.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Globoticket.Services.IntegrationEventPublisher.Repositories
{
    public class IntegrationEventRepository : IIntegrationEventRepository
    {
        private readonly DbContextOptions<IntegrationEventsDbContext> dbContextOptions;

        public IntegrationEventRepository(DbContextOptions<IntegrationEventsDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }

        public async Task<IEnumerable<IntegrationEventLogEntry>> GetUnpublishedEvents()
        {

            await using var _dbContext = new IntegrationEventsDbContext(dbContextOptions);
            return await _dbContext.IntegrationEventLogEntries
                .Where(e => e.State == "Pending").ToListAsync();

        }

        public async Task UpdateIntegrationEventLogEntryState(IntegrationEventLogEntry entry, string state)
        {
            await using var _dbContext = new IntegrationEventsDbContext(dbContextOptions);
            var entryInDatabase = await _dbContext.IntegrationEventLogEntries
                    .Where(e => e.IntegrationEventLogId == entry.IntegrationEventLogId).FirstOrDefaultAsync();
            // could perform optimistic concurrency check to ensure another process hasn't changed the state since last retrieved
            entryInDatabase.State = state;
            await _dbContext.SaveChangesAsync();

        }
    }
}
