using System;
using Contact.IntegrationTests.ProcessorsTests.SupportForTests;
using Contact.Messages.Commands;
using Contact.Messages.Events;
using Core.InProc;
using NServiceBus.Testing;
using NUnit.Framework;
using Rhino.Mocks;

namespace Contact.IntegrationTests.ProcessorsTests
{
    [TestFixture]
    public class TestCreateAuthenticationWithGeneratedPassword
        : WithInProcEventStoreAndNServiceBusPublisher
    {
        [Test]
        public void ShouldPublishAnAuthenticationCreatedEvent()
        {
            Test.Initialize();

            const string generatedPassword = "Password123";
            const string hash = "#;#;#;";
            const string email = "test@test.com";
            var authId = Guid.NewGuid();

            var mockPasswordGenerator = MockRepository.GenerateMock<IGeneratePassword>();
            var mockHasher = MockRepository.GenerateMock<IHash>();
            mockPasswordGenerator.Expect(x => x.GeneratePassword()).Return(generatedPassword);
            mockHasher.Expect(x => x.Hash(generatedPassword)).Return(hash);

            Test.Handler(bus =>
                         new Processors.CreateAuthenticationWithGeneratedPassword
                             (CreateDomainRepository(bus), mockPasswordGenerator, mockHasher))
                .ExpectPublish<AuthenticationCreated>(created =>
                                                      created.AuthenticationID == authId &&
                                                      created.Email == email &&
                                                      created.HashedPassword == hash)
                .OnMessage<CreateAuthenticationWithGeneratedPassword>(password =>
                    {
                        password.AuthID = authId;
                        password.Email = email;
                    });
        }
    }
}