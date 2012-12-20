using System;
using Contact.DomainServices;
using Contact.IntegrationTests.ProcessorsTests.SupportForTests;
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

            Test.Handler<Processors.CreateAuthenticationWithGeneratedPassword>(bus =>
                         new Processors.CreateAuthenticationWithGeneratedPassword
                             (CreateDomainRepository(bus), mockPasswordGenerator, mockHasher))
                .ExpectPublish<Messages.Events.AuthenticationCreated>(created => created.Email == email &&
                                                                                 created.HashedPassword == hash)
                .OnMessage<Messages.Commands.CreateAuthenticationWithGeneratedPassword>(password =>
                    {
                        password.AuthID = authId;
                        password.Email = email;
                    });
        }
    }
}
