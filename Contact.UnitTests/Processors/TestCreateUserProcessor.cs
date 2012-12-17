using System;
using Contact.Infrastructure;
using Contact.Messages.Commands;
using Contact.Messages.Events;
using Contact.Processors;
using NServiceBus.Testing;
using NUnit.Framework;

namespace Contact.UnitTests.Processors
{
    [TestFixture]
    public class TestCreateUserProcessor
    {
        [Test]
        public void ShouldPublishAUserCreatedEvent()
        {
            var correlationId = Guid.NewGuid();
            const string name = "Joe Blogs";

            Test.Initialize();

            Test.Handler<CreateUserProcessor>(bus =>
                                              new CreateUserProcessor(new EventPublisher(bus)))
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
