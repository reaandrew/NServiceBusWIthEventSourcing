using System;
using System.Configuration;
using System.Data.SqlClient;
using Contact.Query.SqlServer.Subscribers;
using NUnit.Framework;

namespace Contact.Query.SqlServer.IntegrationTests
{
    [TestFixture]
    public class TestAccommodationLead
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
                    command.CommandText = "truncate table AccommodationLeads";
                    command.ExecuteNonQuery();
                }
            }
        }

        [Test]
        public void ShouldUpdateTheAccommodationLeadWithApprovedStatus()
        {
            var id = Guid.NewGuid();
            const string name = "Something";
            const string email = "test@test.com";

            //Insert an existing Accommodation Lead
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "insert into AccommodationLeads (AccommodationLeadId,Name,Email) " +
                        "values (@Id, @Name, @Email)";
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.ExecuteNonQuery();
                }
            }

            var handler = new AccommodationLeadApproved(_connectionString);
            var @event = new Messages.Events.AccommodationLeadApproved
                {
                    AccLeadId = id
                };
            handler.Handle(@event);

            //Check the database has been updated
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "Select * from AccommodationLeads where AccommodationLeadId=@Id and Approved=1";
                    command.Parameters.AddWithValue("@Id", id);
                    using (var dataReader = command.ExecuteReader())
                    {
                        Assert.That(dataReader.HasRows, Is.True);
                    }
                }
            }
        }

        [Test]
        public void ShouldCreateANewAccommodationLead()
        {
            var id = Guid.NewGuid();
            const string name = "Something";
            const string email = "test@test.com";

            var handler = new AccommodationLeadCreated(_connectionString);
            var @event = new Contact.Messages.Events.AccommodationLeadCreated
                {
                    AccommodationLeadID = id,
                    Name = name,
                    Email = email
                };
            handler.Handle(@event);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "Select * from AccommodationLeads where AccommodationLeadID = @Id";
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
