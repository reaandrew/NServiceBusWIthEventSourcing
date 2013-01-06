using System;
using Contact.Domain.DomainEvents;
using Contact.Infrastructure.NServiceBus.DomainEventMappers;
using NUnit.Framework;

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
            var domainEvent = new AccommodationLeadApproved
                {
                    ID = id
                };
            Assert.That(mapper.Map(domainEvent), Is.TypeOf<Messages.Events.AccommodationLeadApproved>());
        }
    }
}