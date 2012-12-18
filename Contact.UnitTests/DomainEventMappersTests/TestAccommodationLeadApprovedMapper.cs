using System;
using Contact.Infrastructure.NServiceBus.DomainEventMappers;
using NUnit.Framework;
using AccommodationLeadApproved = Contact.Messages.Events.AccommodationLeadApproved;

namespace Contact.UnitTests.DomainEventMappersTests
{
    [TestFixture]
    public class TestAccommodationLeadApprovedMapper
    {
        [Test]
        public void ShouldReturnABusEventForAccommodationLeadApproved()
        {
            var id = Guid.NewGuid();
            var mapper = new AccommodationLeadApprovedMapper();
            var domainEvent = new Contact.Domain.AccommodationLeadApproved(id);
            Assert.That(mapper.Map(domainEvent), Is.TypeOf<AccommodationLeadApproved>());
        }
    }
}
