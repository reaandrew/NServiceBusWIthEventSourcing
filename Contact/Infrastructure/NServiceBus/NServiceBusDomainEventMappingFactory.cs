using Contact.Core;
using Contact.Infrastructure.NServiceBus.DomainEventMappers;
using NServiceBus;

namespace Contact.Infrastructure.NServiceBus
{
    public class NServiceBusDomainEventMappingFactory
    {
        /// <summary>
        /// Does and doesn't break the OCP principles depending on your view point.
        /// 
        /// Reagrdless this is for the purposes of example so that not external libraries are introduced too soon
        /// 
        /// Be good topics for refactoring with a clear objective.
        /// 
        /// Should not be too much work either...
        /// </summary>
        /// <returns></returns>
        public IDomainEventMappingCollection<IEvent> CreateMappingCollection()
        {
            var mappingCollection = new DomainEventGenericMappingCollection<IEvent>();
            mappingCollection.AddMapping(new UserCreatedMapper());
            mappingCollection.AddMapping(new AccommodationLeadApprovedMapper());
            mappingCollection.AddMapping(new AccommodationLeadCreatedMapper());
            mappingCollection.AddMapping(new AccommodationSupplierCreatedMapper());
            return mappingCollection;
        }
    }
}
