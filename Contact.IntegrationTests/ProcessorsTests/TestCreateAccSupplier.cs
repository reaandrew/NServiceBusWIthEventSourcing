
using Contact.Messages.Events;
using Contact.Processors;
using NServiceBus.Testing;
using NUnit.Framework;

namespace Contact.IntegrationTests.ProcessorsTests
{
    [TestFixture]
    public class TestCreateAccSupplier
    {
        [Test]
        public void ShouldSendCreateAccommodationSupplierCommandWhenAccommodationLeadIsApproved()
        {
            Test.Initialize();
            Test.Saga<CreateAccSupplier>()
                .ExpectSend<Messages.Commands.CreateUser>()
                .When(processor => processor.Handle(new Messages.Commands.CreateAccSupplier
                    {
                        Name = "Something",
                        Email = "test@test.com"
                    }))
                .ExpectPublish<AccSupplierCreated>()
                .When(x => x.Handle(new UserCreated()));
        }
    }
}