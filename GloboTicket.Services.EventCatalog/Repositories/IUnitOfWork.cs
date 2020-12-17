using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboTicket.Services.EventCatalog.Repositories
{
    public interface IUnitOfWork
    {
        IIntegrationEventLogRepository IntegrationEventLogRepository { get; }
        IEventRepository EventRepository { get; }
        void Commit();
        void Rollback();
    }
}
