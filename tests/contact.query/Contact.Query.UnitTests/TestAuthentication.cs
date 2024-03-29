using System;
using Contact.Messages.Events;
using Contact.Query.Contracts.Model;
using Contact.Query.UnitTests.Helpers;
using NUnit.Framework;
using Rhino.Mocks;

namespace Contact.Query.UnitTests
{
    [TestFixture]
    public class TestAuthentication : WithFakeContactQueryRepository
    {
        [Test]
        public void ShouldCreateAuthentication()
        {
            var id = Guid.NewGuid();
            const string email = "something";
            const string hashedPassword = "hash";
            var @event = new AuthenticationCreated
                {
                    AuthenticationID = id,
                    Email = email,
                    HashedPassword = hashedPassword
                };
            var handler = new Subscribers.AuthenticationCreated(Repository);
            handler.Handle(@event);
            Repository.AssertWasCalled(x => x.Save(Arg<Authentication>.Is.Anything));
        }
    }
}