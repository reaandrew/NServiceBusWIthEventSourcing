using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Domain;
using log4net;

namespace Contact.Infrastructure.Logging
{
    public class EventPersistenceWithLogging : IEventPersistence
    {
        private readonly IEventPersistence _persistence;

        public EventPersistenceWithLogging(IEventPersistence persistence)
        {
            _persistence = persistence;
        }

        public void Save(Guid aggregateId, DomainEvent domainEvent)
        {
            try
            {
                _persistence.Save(aggregateId, domainEvent);
            }
            catch(Exception ex)
            {
                LogManager.GetLogger(_persistence.GetType()).Error(ex.ToString());
                throw;
            }
        }

        public IList<DomainEvent> GetEventsForAggregate<T>(Guid id) where T : AggregateRoot
        {
            try
            {
                return _persistence.GetEventsForAggregate<T>(id);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(_persistence.GetType()).Error(ex.ToString());
                throw;
            }
        }
    }
}
