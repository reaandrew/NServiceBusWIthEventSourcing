using System;
using System.Collections.Generic;
using Contact.Core;
using Contact.Domain;
using Contact.Exceptions;

namespace Contact.Infrastructure
{
    public class DomainEventGenericMappingCollection<TDestinationType> : IDomainEventMappingCollection<TDestinationType>
    {
        private readonly Dictionary<Type, object> _mappings;

        public DomainEventGenericMappingCollection()
        {
            _mappings = new Dictionary<Type, object>();
        }

        public void AddMapping<TDomainEventType>(IMapDomainEvent<TDomainEventType,TDestinationType> eventMapper)
            where TDomainEventType : DomainEvent
        {
            _mappings.Add(typeof (TDomainEventType), eventMapper);
        }

        public TDestinationType GetMappedEventFor<TDomainEventType>(TDomainEventType domainEvent)
        {
            var domainEventType = typeof (TDomainEventType);
            if (!_mappings.ContainsKey(domainEventType))
                throw new DomainEventNotFoundException();

            return ((IMapDomainEvent<TDomainEventType, TDestinationType>)
                    _mappings[domainEventType]).Map(domainEvent);
        }
    }
}