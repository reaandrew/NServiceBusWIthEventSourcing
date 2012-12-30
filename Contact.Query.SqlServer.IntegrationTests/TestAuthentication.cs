using System;
using System.Configuration;
using System.Data.SqlClient;
using NUnit.Framework;

namespace Contact.Query.SqlServer.IntegrationTests
{
    [TestFixture]
    public class TestAuthentication
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
                    command.CommandText = "truncate table Authentication";
                    command.ExecuteNonQuery();
                }
            }
        }

        [Test]
        public void ShouldCreateAuthentication()
        {
            var id = Guid.NewGuid();
            const string email = "something";
            const string hashedPassword = "hash";
            var @event = new Contact.Messages.Events.AuthenticationCreated
                {
                    AuthenticationID = id,
                    Email = email,
                    HashedPassword = hashedPassword
                };
            var handler = new Subscribers.AuthenticationCreated();
            handler.Handle(@event);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Authentication where AuthenticationId=@AuthId";
                    command.Parameters.AddWithValue("@AuthId", id);
                    using (var dataReader = command.ExecuteReader())
                    {
                        dataReader.Read();
                        Assert.That(dataReader.GetString(dataReader.GetOrdinal("Email")),Is.EqualTo(email));
                        Assert.That(dataReader.GetString(dataReader.GetOrdinal("HashedPassword")), Is.EqualTo(hashedPassword));
                    }
                }
            }
        }
    }
}
