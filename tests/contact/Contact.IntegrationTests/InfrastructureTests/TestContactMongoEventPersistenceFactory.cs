using Contact.Infrastructure.Mongo;
using Core.Configuration;
using NUnit.Framework;

namespace Contact.IntegrationTests.InfrastructureTests
{
    [TestFixture]
    public class TestContactMongoEventPersistenceFactory
    {
        [Test]
        public void ShouldReturnInstanceOfEventStoreFactory()
        {
            var factory = EventPersistenceFactoryConfiguration.CreateFactory();
            Assert.That(factory, Is.TypeOf<MongoContactEventPersistenceFactory>());
        }
    }
}