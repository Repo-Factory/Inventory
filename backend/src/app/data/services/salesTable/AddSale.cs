using Npgsql;

namespace DataBase
{
    class AddSaleService
    {
        public static void AddSale(in string name, in DateTime time, in float sell_price)
        {
            try
            {
                using NpgsqlConnection connection = ConnectionHelper.Connect(); connection.Open();
                /* const */ string query = BuildInsertQuery();
                using NpgsqlCommand command = new(query, connection);
                AddParameters(command, name, time, sell_price);
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
                        INSERT INTO sales (Name, Time, Sell_price)
                        VALUES (@Name, @Time, @SellPrice);"
                    ;
        }

        private static void AddParameters(in NpgsqlCommand command, in string name, in DateTime time, in float sell_price)
        {
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Time", time);
            command.Parameters.AddWithValue("@SellPrice", sell_price);
        }
    }
}
