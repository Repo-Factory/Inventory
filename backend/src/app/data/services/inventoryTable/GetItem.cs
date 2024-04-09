using InventoryItem = Models.Inventory.Item;
using Npgsql;

namespace DataBase
{
    class GetItemService
    {
        public static List<InventoryItem> GetItem(in string name)
        {
            List<InventoryItem> items = [];
            try
            {
                using NpgsqlConnection connection = ConnectionHelper.Connect(); connection.Open();
                /* const */ string query = BuildGetItemQuery();
                using NpgsqlCommand command = new(query, connection);
                AddParameters(command, name);
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
        private static string BuildGetItemQuery()
        {
            return  @$" 
                        SELECT * 
                        FROM inventory 
                        WHERE name=@name;"
                    ;
        }
        private static void AddParameters(NpgsqlCommand command, string name)
        {
            command.Parameters.AddWithValue("@Name", name);
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