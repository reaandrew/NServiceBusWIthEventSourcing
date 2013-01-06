using System;
using Contact.IntegrationTests.ProcessorsTests.SupportForTests;
using Contact.Messages.Commands;
using Contact.Messages.Events;
using NServiceBus.Testing;
using NUnit.Framework;
using CreateAccSupplier = Contact.Processors.CreateAccSupplier;

namespace Contact.IntegrationTests.ProcessorsTests
{
    [TestFixture]
    public class TestCreateAccSupplier : WithInProcEventStoreAndNServiceBusPublisher
    {
        [Test]
        public void ShouldSendCreateAccommodationSupplierCommandWhenAccommodationLeadIsApproved()
        {
            const string accSupplierEmail = "test@test.com";
            const string accSupplierName = "Somethign";
            var authId = Guid.NewGuid();

            Test.Initialize();
            Test.Saga<CreateAccSupplier>()
                .WithExternalDependencies(supplier => supplier.DomainRepository = CreateDomainRepository(supplier.Bus))
                .ExpectSend<CreateAuthenticationWithGeneratedPassword>
                (password => password.Email == accSupplierEmail)
                .When(processor => processor.Handle(new Messages.Commands.CreateAccSupplier
                    {
                        Name = accSupplierName,
                        Email = accSupplierEmail
                    }))
                .ExpectSend<CreateUser>(user => user.Name == accSupplierName &&
                                                user.Email == accSupplierEmail)
                .When(processor => processor.Handle(new AuthenticationCreated
                    {
                        AuthenticationID = authId,
                        Email = accSupplierEmail,
                        HashedPassword = "OEHOFEHOIEFH"
                    }))
                .ExpectPublish<AccommodationSupplierCreated>()
                .When(x => x.Handle(new UserCreated()));
        }
    }
}