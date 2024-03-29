using Contact.Messages.Events;
using Contact.Subscribers;
using Core.InProc;
using NUnit.Framework;
using Rhino.Mocks;

namespace Contact.IntegrationTests.SubscribersTests
{
    [TestFixture]
    public class TestSendAccSupplierRegistrationEmail
    {
        [Test]
        public void ShouldSendConfirmationOfRegistrationToAccommodationSupplier()
        {
            var fakeSender = MockRepository.GenerateMock<ISendEmails>();
            var handler = new SendAccSupplierRegistrationEmail(fakeSender);
            handler.Handle(new AccommodationSupplierCreated
                {
                    Name = "Somebody"
                });
            fakeSender.AssertWasCalled(emails => emails.SendEmail(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
    }
}