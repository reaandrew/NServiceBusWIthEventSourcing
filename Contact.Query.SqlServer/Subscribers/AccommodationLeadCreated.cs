using System;
using System.Configuration;
using System.Data.SqlClient;
using NServiceBus;

namespace Contact.Query.SqlServer.Subscribers
{
    public class AccommodationLeadCreated : IHandleMessages<Contact.Messages.Events.AccommodationLeadCreated>
    {
        private readonly string _connectionString;

        public AccommodationLeadCreated(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Handle(Messages.Events.AccommodationLeadCreated message)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "insert into AccommodationLeads (AccommodationLeadId, Name, Email) values (@AccommodationLeadId,@name,@Email)";
                    command.Parameters.AddWithValue("@AccommodationLeadId", message.AccommodationLeadID);
                    command.Parameters.AddWithValue("@Name", message.Name);
                    command.Parameters.AddWithValue("@Email", message.Email);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
