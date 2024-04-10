using Npgsql;
using Sale = Models.Inventory.Sale;

namespace DataBase
{
    class GetSalesService
    {
        public static List<Sale> GetSales(in DateTime before, in DateTime after)
        {
            List<Sale> sales = [];
            try
            {
                using NpgsqlConnection connection = ConnectionHelper.Connect(); connection.Open();
                /* const */ string query = BuildGetSaleQuery();
                using NpgsqlCommand command = new(query, connection);
                AddParameters(command, before, after);
                using NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Sale sale = BuildSale(reader);
                    sales.Add(sale);
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return sales;
        }
        private static string BuildGetSaleQuery()
        {
            return  @$" 
                        SELECT * 
                        FROM sales 
                        WHERE time < @before AND time > @after;"
                    ;
        }
        private static void AddParameters(NpgsqlCommand command, DateTime before, DateTime after)
        {
            command.Parameters.AddWithValue("@before", before);
            command.Parameters.AddWithValue("@after", after);
        }
        private static Sale BuildSale(NpgsqlDataReader reader)
        {
            return new(
                reader.GetString(reader.GetOrdinal("name")),
                reader.GetDateTime(reader.GetOrdinal("time")),
                reader.GetFloat(reader.GetOrdinal("sell_price"))
            );
        }
    }
}
