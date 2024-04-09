using Npgsql;

namespace DataBase
{
    class UpdateItemService
    {
        public static void Update(string name, int quantity, float cost_basis)
        {
            try
            {
                using NpgsqlConnection connection = ConnectionHelper.Connect(); connection.Open();
                /* const */ string query = BuildUpdateQuery();
                using NpgsqlCommand command = new(query, connection);
                AddParameters(command, name, quantity, cost_basis);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static string BuildUpdateQuery()
        {
            return @"
                        UPDATE inventory
                        SET quantity=@Quantity, cost_basis=@CostBasis
                        WHERE name=@Name;";
        }

        private static void AddParameters(NpgsqlCommand command, string name, int quantity, float cost_basis)
        {
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@CostBasis", cost_basis);
        }
    }
}
