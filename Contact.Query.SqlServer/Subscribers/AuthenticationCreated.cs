using System.Data.SqlClient;
using NServiceBus;

namespace Contact.Query.SqlServer.Subscribers
{
    public class AuthenticationCreated : IHandleMessages<Contact.Messages.Events.AuthenticationCreated>
    {
        private readonly string _connectionString;

        public AuthenticationCreated(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Handle(Messages.Events.AuthenticationCreated message)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "insert into Authentication (AuthenticationId,Email,HashedPassword) values (@AuthId,@Email,@HashedPassword)";
                    command.Parameters.AddWithValue("@AuthId", message.AuthenticationID);
                    command.Parameters.AddWithValue("@Email", message.Email);
                    command.Parameters.AddWithValue("@HashedPassword", message.HashedPassword);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
