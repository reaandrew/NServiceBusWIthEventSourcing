using System.Data.SqlClient;
using NServiceBus;

namespace Contact.Query.SqlServer.Subscribers
{
    public class AccommodationLeadApproved : IHandleMessages<Messages.Events.AccommodationLeadApproved>
    {
        private readonly string _connectionString;

        public AccommodationLeadApproved(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Handle(Messages.Events.AccommodationLeadApproved message)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "Update AccommodationLeads set Approved = 1 where AccommodationLeadId = @Id";
                    command.Parameters.AddWithValue("@Id", message.AccLeadId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
