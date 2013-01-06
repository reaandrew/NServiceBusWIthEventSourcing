using System;
using Contact.Domain.DomainEvents;
using Contact.Infrastructure.NServiceBus.DomainEventMappers;
using NUnit.Framework;

namespace Contact.UnitTests.DomainEventMappersTests
{
    [TestFixture]
    public class TestAccommodationLeadCreatedMapper
    {
        [Test]
        public void ShouldReturnABusEventForAccommodationLeadCreated()
        {
            var id = Guid.NewGuid();
            var mapper = new AccommodationLeadCreatedMapper();
            var domainEvent = new AccommodationLeadCreated
                {
                    ID = id,
                    Name = "Something",
                    Email = "test@test.com"
                };
            Assert.That(mapper.Map(domainEvent), Is.TypeOf<Messages.Events.AccommodationLeadCreated>());
        }
    }
}