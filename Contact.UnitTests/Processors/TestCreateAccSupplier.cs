using Contact.Messages.Commands;
using Contact.Messages.Events;
using NServiceBus.Testing;
using NUnit.Framework;
using CreateAccSupplier = Contact.Processors.CreateAccSupplier;

namespace Contact.UnitTests.Processors
{
    [TestFixture]
    public class TestCreateAccSupplier
    {
        [Test]
        public void ShouldSendCreateAccommodationSupplierCommandWhenAccommodationLeadIsApproved()
        {
            Test.Initialize();
            Test.Saga<CreateAccSupplier>()
                .ExpectSend<CreateUser>()
                .When(processor => processor.Handle(new Messages.Commands.CreateAccSupplier
                    {
                        Name = "Something"
                    }))
                .ExpectPublish<AccSupplierCreated>()
                .When(x => x.Handle(new UserCreated()));
        }
    }
}