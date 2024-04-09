using Microsoft.Data.SqlClient;
using InventoryItem = Models.Inventory.Item;

namespace DataBase
{
    class Query
    {
        public static void Update(in string query)
        {
            try 
            {
                using SqlConnection connection = Connect(); connection.Open();
                using SqlCommand command = new(query, connection);
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public static List<InventoryItem> Read(in string query)
        {
            List<InventoryItem> items = [];
            try
            {
                using SqlConnection connection = Connect();
                connection.Open();
                using SqlCommand command = new(query, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    InventoryItem item = new(reader.GetString(reader.GetOrdinal("Name")))
                    {
                        Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                        Cost_basis = reader.GetFloat(reader.GetOrdinal("Cost_basis"))
                    };
                    items.Add(item);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return items;
        }
        private static SqlConnection Connect()
        {
            SqlConnectionStringBuilder builder = new()
            {
                DataSource = "localhost",
                UserID = "docker_postgres",
                Password = "docker_postgres",
                InitialCatalog = "docker_postgres"
            };
            return new(builder.ConnectionString);
        }
    }
}