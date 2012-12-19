using System;
using System.Collections.Generic;
using System.Linq;
using Contact.Domain;
using Contact.Exceptions;
using NServiceBus;

namespace Contact.Infrastructure.NServiceBus
{
    //This is just for the purposes of example
    //Using a dedicated mapping library would be good in the future to make use of
    //IL Generation optimizations etc... but leaving out as much external deps
    //as possible for this example to show to lack of dependency on them.
    //Performance with this implementation will be fine but will obviously not be
    //the best.
    public class NServiceBusEventMappings : IEventMappings
    {
        private readonly List<IEventMapper> _eventMappers;
 
        public NServiceBusEventMappings()
        {
            _eventMappers = new List<IEventMapper>();
        }
        public IEvent GetMappedObjectFor(DomainEvent domainEvent)
        {
            var mapper = _eventMappers.SingleOrDefault(x => x.CanMap(domainEvent));
            if(mapper == null)
                throw new MapperNotFoundException();
            return mapper.Map(domainEvent);
        }

        public void AddMapper(IEventMapper mapper)
        {
            _eventMappers.Add(mapper);
        }
    }
}