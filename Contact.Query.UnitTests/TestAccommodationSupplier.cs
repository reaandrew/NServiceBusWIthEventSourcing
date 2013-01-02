using System;
using System.Data.SqlClient;
using Contact.Query.Contracts.Model;
using Contact.Query.UnitTests.Helpers;
using NUnit.Framework;
using Rhino.Mocks;

namespace Contact.Query.UnitTests
{
    [TestFixture]
    public class TestAccommodationSupplier : WithFakeContactQueryRepository
    {
        [Test]
        public void ShouldCreatedAnAccommmodationSupplier()
        {
            var id = Guid.NewGuid();
            const string name = "test";
            const string email = "test@test.com";

            var @event = new Contact.Messages.Events.AccommodationSupplierCreated
                {
                    AccommodationSupplierId = id,
                    Name = name,
                    Email = email
                };
            var handler = new Subscribers.AccommodationSupplierCreated(Repository);
            handler.Handle(@event);
            Repository.AssertWasCalled(x => x.Save(Arg<AccommodationSupplier>.Is.Anything));
        }
    }
}
