using System;
using System.Collections.Generic;
using Core;
using Core.Domain;
using Core.DomainServices;

namespace Contact.Infrastructure.InProc
{
    public class InProcEventPersistence : IEventPersistence
    {
        private readonly Dictionary<Guid, IList<DomainEvent>> _persistence;

        public InProcEventPersistence()
        {
            _persistence = new Dictionary<Guid, IList<DomainEvent>>();
        }

        public void Save(Guid aggregateId, DomainEvent domainEvent)
        {
            if (!_persistence.ContainsKey(aggregateId))
            {
                _persistence.Add(aggregateId, new List<DomainEvent>());
            }

            _persistence[aggregateId].Add(domainEvent);
        }

        public IList<DomainEvent> GetEventsForAggregate<T>(Guid id) where T : AggregateRoot
        {
            IList<DomainEvent> returnList = null;
            returnList = !_persistence.ContainsKey(id) ? new List<DomainEvent>() 
                             : _persistence[id];

            return returnList;
        }
    }
}