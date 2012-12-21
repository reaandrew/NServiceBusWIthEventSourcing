using System;
using Contact.Infrastructure.NServiceBus.DomainEventMappers;
using NUnit.Framework;
using UserCreated = Contact.Messages.Events.UserCreated;

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
            var domainEvent = new Domain.DomainEvents.UserCreated
                {
                    ID = id,
                    Name = "Something",
                    Email = "test@test.com"
                };
            Assert.That(mapper.Map(domainEvent), Is.TypeOf<UserCreated>());
        }
    }
}