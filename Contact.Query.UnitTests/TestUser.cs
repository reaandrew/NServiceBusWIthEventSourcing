using System;
using Contact.Query.Model;
using Contact.Query.UnitTests.Helpers;
using NUnit.Framework;
using Rhino.Mocks;

namespace Contact.Query.UnitTests
{
    [TestFixture]
    public class TestUser : WithFakeContactQueryRepository
    {
        [Test]
        public void ShouldCreateANewUser()
        {
            const string name = "Test";
            const string email = "test@test.com";
            var userId = Guid.NewGuid();
            var @event = new Contact.Messages.Events.UserCreated
                {
                    UserID = userId,
                    Name = name,
                    Email = email
                };
            var handler = new Subscribers.UserCreated(Repository);
            handler.Handle(@event);
            Repository.AssertWasCalled(x => x.Save(Arg<User>.Is.Anything));
        }
    }
}
