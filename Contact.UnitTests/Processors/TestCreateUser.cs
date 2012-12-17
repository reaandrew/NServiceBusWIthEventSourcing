using System;
using Contact.Infrastructure;
using Contact.Messages.Commands;
using Contact.Messages.Events;
using Contact.Processors;
using NServiceBus.Testing;
using NUnit.Framework;
using CreateUser = Contact.Processors.CreateUser;

namespace Contact.UnitTests.Processors
{
    [TestFixture]
    public class TestCreateUser
    {
        [Test]
        public void ShouldPublishAUserCreatedEvent()
        {
            var correlationId = Guid.NewGuid();
            const string name = "Joe Blogs";

            Test.Initialize();

            Test.Handler<CreateUser>(bus =>
                                              new CreateUser(new EventPublisher(bus)))
                .ExpectPublish<UserCreated>(created => created.CorrelationId == correlationId &&
                                                       created.Name == name)
                .OnMessage<Messages.Commands.CreateUser>(user =>
                    {
                        user.CorrelationId = correlationId;
                        user.Name = name;
                    });
        }
    }
}
