using Contact.Infrastructure.Mongo;
using Core.Configuration;
using NUnit.Framework;

namespace Core.IntegrationTests
{
    [TestFixture]
    public class TestMongoEventPersistenceFactory
    {
        [Test]
        public void ShouldReturnInstanceOfEventStoreFactory()
        {
            var factory = EventPersistenceFactoryConfiguration.CreateFactory();
            Assert.That(factory, Is.TypeOf<MongoContactEventPersistenceFactory>());
        }
    }

}
