using Microsoft.Data.SqlClient;

namespace DataBase
{
    class Query
    {
        public static void Update(in string query)
        {
            try 
            {
                SqlConnection connection = Connect(); connection.Open();
                using SqlCommand command = new(query, connection);
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public static List<Models.Inventory.InventoryItem> Read(in string query)
        {
            try 
            {
                SqlConnection connection = Connect(); connection.Open();
                using SqlCommand command = new(query, connection);
                using SqlDataReader reader =  command.ExecuteReader();
                while (reader.Read())
                {
                    
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return ;
            }
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