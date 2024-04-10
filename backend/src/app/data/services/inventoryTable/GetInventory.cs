using InventoryItem = Models.Inventory.Item;
using Npgsql;

namespace DataBase
{
    class GetInventoryService
    {
        public static List<InventoryItem> GetInventory()
        {
            List<InventoryItem> items = [];
            try
            {
                using NpgsqlConnection connection = ConnectionHelper.Connect(); connection.Open();
                /* const */ string query = BuildGetInventoryQuery();
                using NpgsqlCommand command = new(query, connection);
                using NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    InventoryItem item = BuildItem(reader);
                    items.Add(item);
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return items;
        }
        private static string BuildGetInventoryQuery()
        {
            return  @$" 
                        SELECT * 
                        FROM inventory;"
                    ;
        }
        private static InventoryItem BuildItem(NpgsqlDataReader reader)
        {
            return new(reader.GetString(reader.GetOrdinal("name")))
            {
                Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                Cost_basis = reader.GetFloat(reader.GetOrdinal("cost_basis"))
            };
        }
    }
}