using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Contact.Query.SqlServer.Subscribers
{
    public class UserCreated : IHandleMessages<Contact.Messages.Events.UserCreated>
    {
        private readonly string _connectionString;

        public UserCreated(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Handle(Messages.Events.UserCreated message)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "insert into Users (UserId,Name,Email) values (@UserId,@Name,@Email)";
                    command.Parameters.AddWithValue("@UserId", message.UserID);
                    command.Parameters.AddWithValue("@Name", message.Name);
                    command.Parameters.AddWithValue("@Email", message.Email);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
