using Contact.Infrastructure;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;

namespace Contact.UnitTests.Infrastructure
{
    [TestFixture]
    public class TestEventPublisher
    {
        [Test]
        public void ShouldPublishEvent()
        {
            var mockBus = MockRepository.GenerateMock<IBus>();
            var someEvent = new object();
            var eventPublisher = new EventPublisher(mockBus);
            eventPublisher.Publish(someEvent);
            mockBus.AssertWasCalled(x => x.Publish(someEvent));
        }
    }
}
