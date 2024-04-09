using InventoryItem = Models.Inventory.Item;

namespace Sales 
{
    class Operations
    {
        public static void BuyProduct(in string name, in float price)
        {
            /* const */ string readQuery = BuildReadQuery(name);
            /* const */ List<InventoryItem> items = DataBase.Query.Read(readQuery);
            /* const */ InventoryItem item = HandleNewItem(items, name);
            /* const */ int current_quantity = item.Quantity;
            /* const */ float cost_basis = UpdateCostBasis(items[0].Cost_basis, current_quantity, price);
            /* const */ string writeQuery = BuildWriteQuery(current_quantity, cost_basis);
            DataBase.Query.Update(writeQuery);
        }
        private static InventoryItem HandleNewItem(in List<InventoryItem> items, in string name)
        {
            if (items.Count == 0) {
                return new InventoryItem(name);
            }
            else {
                return items[0];
            }
        }
        private static string BuildReadQuery(in string name)
        {
            return  @$" 
                        SELECT quantity 
                        FROM inventory 
                        WHERE name=={name}"
                    ;   
        }
        private static string BuildWriteQuery(in int current_quantity, in float cost_basis)
        {
            return  @$" UPDATE inventory
                        SET quantity={current_quantity+1}, cost_basis={cost_basis}
                        WHERE condition;"
                    ;
        }
        private static float UpdateCostBasis(in float cost_basis, in int quantity, in float price)
        {
            /* const */ float avg_cost = quantity/cost_basis;
            /* const */ float total_cost = avg_cost+price;
            /* const */ float updated_quantity = quantity+1;
            return total_cost/updated_quantity;
        }

        public static void SellProduct(in string name)
        {
            try
            {
                /* const */ string readQuery = BuildReadQuery(name);
                /* const */ List<InventoryItem> items = DataBase.Query.Read(readQuery);
                /* const */ InventoryItem item = HandleNonExistentProduct(items, name);
                /* const */ int quantity = item.Quantity;
                /* const */ string writeQuery = BuildWriteQuery(quantity);
                DataBase.Query.Update(writeQuery);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static InventoryItem HandleNonExistentProduct(List<InventoryItem> items, string name)
        {
            if (items.Count == 0)
            {
                throw new InvalidOperationException($"No item with name '{name}' exists.");
            }
            else
            {
                return items[0];
            }
        }
        private static string BuildWriteQuery(in int quantity)
        {
            return  @$" UPDATE inventory
                        SET quantity={quantity-1},
                        WHERE condition;"
                    ;
        }
    }
}