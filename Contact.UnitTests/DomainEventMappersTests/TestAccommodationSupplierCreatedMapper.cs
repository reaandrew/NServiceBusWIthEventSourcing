using System;
using Contact.Domain.DomainEvents;
using Contact.Infrastructure.NServiceBus.DomainEventMappers;
using Contact.Messages.Events;
using NUnit.Framework;

namespace Contact.UnitTests.DomainEventMappersTests
{
    [TestFixture]
    public class TestAccommodationSupplierCreatedMapper
    {
        [Test]
        public void ShouldReturnABusEventForAccommodationSupplierCreated()
        {
            var id = Guid.NewGuid();
            var mapper = new AccommodationSupplierCreatedMapper();
            var domainEvent = new AccommodationSupplierCreated
                {
                    ID = id,
                    Name = "Something",
                    Email = "test@test.com"
                };
            Assert.That(mapper.Map(domainEvent), Is.TypeOf<AccSupplierCreated>());
        }
    }
}