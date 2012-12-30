using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Contact.Query.SqlServer.IntegrationTests
{
    [TestFixture]
    public class TestAccommodationSupplier
    {
        private string _connectionString;
        [SetUp]
        public void Setup()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["TestDatabase"].ConnectionString;
        }

        [TearDown]
        public void TearDown()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "truncate table AccommodationSuppliers";
                    command.ExecuteNonQuery();
                }
            }
        }

        [Test]
        public void ShouldCreatedAnAccommmodationSupplier()
        {
            var id = Guid.NewGuid();
            var name = "test";
            var email = "test@test.com";

            var @event = new Contact.Messages.Events.AccommodationSupplierCreated
                {
                    AccommodationSupplierId = id,
                    Name = name,
                    Email = email
                };
            var handler = new Subscribers.AccommodationSupplierCreated();
            handler.Handle(@event);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select * from AccommodationSuppliers where AccommodationSupplierId=@Id";
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        Assert.That(reader.GetString(reader.GetOrdinal("Name")), Is.EqualTo(name));
                        Assert.That(reader.GetString(reader.GetOrdinal("Email")), Is.EqualTo(email));
                    }
                }
            }

        }
    }
}
