using System;
using Contact.Infrastructure;
using Contact.Messages.Commands;
using Contact.Messages.Events;
using NServiceBus.Testing;
using NUnit.Framework;

namespace Contact.UnitTests.Processors
{
    [TestFixture]
    public class TestCreateUser
    {
        [Test]
        public void ShouldPublishAUserCreatedEvent()
        {
            Guid correlationId = Guid.NewGuid();
            const string name = "Joe Blogs";

            Test.Initialize();

            Test.Handler(bus =>
                         new Contact.Processors.CreateUser(new EventPublisher(bus)))
                .ExpectPublish<UserCreated>(created => created.CorrelationId == correlationId &&
                                                       created.Name == name)
                .OnMessage<CreateUser>(user =>
                    {
                        user.CorrelationId = correlationId;
                        user.Name = name;
                    });
        }
    }
}