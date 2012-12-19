using System;
using Contact.IntegrationTests.ProcessorsTests.SupportForTests;
using Contact.Messages.Commands;
using Contact.Messages.Events;
using NServiceBus.Testing;
using NUnit.Framework;

namespace Contact.IntegrationTests.ProcessorsTests
{
    [TestFixture]
    public class TestCreateUser : WithInProcEventStoreAndNServiceBusPublisher
    {
        [Test]
        public void ShouldPublishAUserCreatedEvent()
        {
            var userID = Guid.NewGuid();
            const string name = "Joe Blogs";
            const string email = "test@test.com";

            Test.Initialize();

            Test.Handler(bus =>
                {
                    var domainRepository = CreateDomainRepository(bus);
                    return new Processors.CreateUser(domainRepository);
                })
                .ExpectPublish<UserCreated>(created =>
                    created.UserID == userID &&
                    created.Name == name &&
                    created.Email == email)
                .OnMessage<CreateUser>(user =>
                    {
                        user.UserId = userID;
                        user.Name = name;
                        user.Email = email;
                    });
        }
    }
}