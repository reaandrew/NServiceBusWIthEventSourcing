using System;
using Contact.Domain.DomainEvents;
using Contact.Infrastructure.NServiceBus.DomainEventMappers;
using NUnit.Framework;

namespace Contact.UnitTests.DomainEventMappersTests
{
    [TestFixture]
    public class TestUserCreatedMapper
    {
        [Test]
        public void ShouldReturnABusEventForUserCreated()
        {
            var id = Guid.NewGuid();
            var mapper = new UserCreatedMapper();
            var domainEvent = new UserCreated
                {
                    ID = id,
                    Name = "Something",
                    Email = "test@test.com"
                };
            Assert.That(mapper.Map(domainEvent), Is.TypeOf<Messages.Events.UserCreated>());
        }
    }
}