using System.Data.SqlClient;
using NServiceBus;

namespace Contact.Query.SqlServer.Subscribers
{
    public class AccommodationSupplierCreated : IHandleMessages<Contact.Messages.Events.AccommodationSupplierCreated>
    {
        private readonly string _connectionString;

        public AccommodationSupplierCreated(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Handle(Messages.Events.AccommodationSupplierCreated message)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "insert into AccommodationSuppliers (AccommodationSupplierId,Name,Email) values (@Id,@Name,@Email)";
                    command.Parameters.AddWithValue("@Id", message.AccommodationSupplierId);
                    command.Parameters.AddWithValue("@Name", message.Name);
                    command.Parameters.AddWithValue("@Email", message.Email);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
