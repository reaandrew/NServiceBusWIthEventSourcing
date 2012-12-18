using NUnit.Framework;

namespace Contact.IntegrationTests.ProcessorsTests
{
    [TestFixture]
    public class TestCreateUser
    {
        [Test]
        public void ShouldPublishAUserCreatedEvent()
        {
            Assert.Inconclusive();
            //Guid correlationId = Guid.NewGuid();
            //const string name = "Joe Blogs";

            //Test.Initialize();

            //Test.Handler(bus =>
            //             new Contact.Processors.CreateUser(new NServiceBusEventPublisher(bus)))
            //    .ExpectPublish<UserCreated>(created => created.CorrelationId == correlationId &&
            //                                           created.Name == name)
            //    .OnMessage<CreateUser>(user =>
            //        {
            //            user.CorrelationId = correlationId;
            //            user.Name = name;
            //        });
        }
    }
}