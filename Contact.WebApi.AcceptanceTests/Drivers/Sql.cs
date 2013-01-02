using System.Configuration;
using System.Data.SqlClient;

namespace Contact.WebApi.AcceptanceTests.Drivers
{
    public class Sql : ITestDataDriver
    {
        public void DeleteAllTestData()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["TestDatabase"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"truncate table AccommodationLeads;
truncate table AccommodationSuppliers;
truncate table Authentication;
truncate table Users;";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
