using Npgsql;

namespace DataBase
{
    class AddItemService
    {
        public static void AddItem(string name, int quantity, float cost_basis)
        {
            try
            {
                using NpgsqlConnection connection = ConnectionHelper.Connect(); connection.Open();
                /* const */ string query = BuildInsertQuery();
                using NpgsqlCommand command = new(query, connection);
                AddParameters(command, name, quantity, cost_basis);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static string BuildInsertQuery()
        {
            return @"
                        INSERT INTO inventory (Name, Quantity, Cost_basis)
                        VALUES (@Name, @Quantity, @CostBasis);"
                    ;
        }

        private static void AddParameters(NpgsqlCommand command, string name, int quantity, float cost_basis)
        {
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@CostBasis", cost_basis);
        }
    }
}
