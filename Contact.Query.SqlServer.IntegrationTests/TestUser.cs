using System;
using System.Configuration;
using System.Data.SqlClient;
using NUnit.Framework;

namespace Contact.Query.SqlServer.IntegrationTests
{
    [TestFixture]
    public class TestUser
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
                    command.CommandText = "truncate table Users";
                    command.ExecuteNonQuery();
                }
            }
        }

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
            var handler = new Subscribers.UserCreated();
            handler.Handle(@event);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select * from Users where UserId=@UserId";
                    command.Parameters.AddWithValue("@UserId", userId);
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
