using System;
using System.Configuration;
using System.Data.SqlClient;
using NUnit.Framework;

namespace Contact.Query.SqlServer.IntegrationTests
{
    [TestFixture]
    public class TestContactQueryRepository
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
        public void ShouldSaveAccommodationLead()
        {
            var id = Guid.NewGuid();
            const string name = "Something";
            const string email = "test@test.com";
            const bool approved = false;
            var accommodationLead = new Contracts.Model.AccommodationLead
                {
                    AccommodationLeadId = id,
                    Name = name,
                    Email = email,
                    Approved = approved
                };
            var repository = new ContactQueryRepository();
            repository.Save(accommodationLead);
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select * from AccommodationLeads where AccommodationLeadId=@Id";
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        Assert.That(reader.GetString(reader.GetOrdinal("Name")), Is.EqualTo(name));
                        Assert.That(reader.GetString(reader.GetOrdinal("Email")), Is.EqualTo(email));
                        Assert.That(reader.GetBoolean(reader.GetOrdinal("Approved")), Is.EqualTo(approved));
                    }
                }
            }
        }
    }
}