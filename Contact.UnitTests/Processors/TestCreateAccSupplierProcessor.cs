using Contact.Messages.Commands;
using Contact.Processors;
using NServiceBus.Testing;
using NUnit.Framework;

namespace Contact.UnitTests.Processors
{
    [TestFixture]
    public class TestCreateAccSupplierProcessor
    {
        [Test]
        public void ShouldSendCreateAccommodationSupplierCommandWhenAccommodationLeadIsApproved()
        {
            Test.Initialize();
            Test.Saga<CreateAccSupplierProcessor>()
                .ExpectSend<CreateUser>()
                .When(processor => processor.Handle(new CreateAccSupplier
                    {
                        Name = "Something"
                    }));
        }
    }
}
