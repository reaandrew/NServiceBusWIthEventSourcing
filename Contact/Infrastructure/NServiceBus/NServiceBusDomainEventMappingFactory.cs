using Contact.Infrastructure.NServiceBus.DomainEventMappers;
using Infrastructure.NServiceBus;

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
        public IEventMappings CreateMappingCollection()
        {
            var mappingCollection = new NServiceBusEventMappings();
            mappingCollection.AddMapper(new UserCreatedMapper());
            mappingCollection.AddMapper(new AccommodationLeadApprovedMapper());
            mappingCollection.AddMapper(new AccommodationLeadCreatedMapper());
            mappingCollection.AddMapper(new AccommodationSupplierCreatedMapper());
            mappingCollection.AddMapper(new AuthenticationCreatedMapper());
            return mappingCollection;
        }
    }
}