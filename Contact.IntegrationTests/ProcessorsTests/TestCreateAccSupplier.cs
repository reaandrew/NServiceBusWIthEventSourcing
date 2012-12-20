
using System;
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
            const string accSupplierEmail = "test@test.com";
            const string accSupplierName = "Somethign";
            var authId = Guid.NewGuid();

            Test.Initialize();
            Test.Saga<CreateAccSupplier>()
                .ExpectSend<Messages.Commands.CreateAuthenticationWithGeneratedPassword>
                (password => password.Email == accSupplierEmail)
                .When(processor => processor.Handle(new Messages.Commands.CreateAccSupplier
                    {
                        Name = accSupplierName,
                        Email = accSupplierEmail
                    }))
                .ExpectSend<Messages.Commands.CreateUser>(user => user.Name == accSupplierName &&
                                                                  user.Email == accSupplierEmail)
                .When(processor => processor.Handle(new AuthenticationCreated
                    {
                        AuthenticationID = authId,
                        Email = accSupplierEmail,
                        HashedPassword = "OEHOFEHOIEFH"
                    }))
                .ExpectPublish<AccSupplierCreated>()
                .When(x => x.Handle(new UserCreated()));
        }
    }
}