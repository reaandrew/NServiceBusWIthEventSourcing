using Contact.Core;
using Contact.Messages.Events;
using Contact.Subscribers;
using NUnit.Framework;
using Rhino.Mocks;

namespace Contact.UnitTests.SubscribersTests
{
    [TestFixture]
    public class TestSendAccSupplierRegistrationEmail
    {
        [Test]
        public void ShouldSendConfirmationOfRegistrationToAccommodationSupplier()
        {
            var fakeSender = MockRepository.GenerateMock<ISendEmails>();
            var handler = new SendAccSupplierRegistrationEmail(fakeSender);
            handler.Handle(new AccSupplierCreated
                {
                    Name = "Somebody"
                });
            fakeSender.AssertWasCalled(emails => emails.SendEmail(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
    }
}