using InventoryItem = Models.Inventory.Item;

namespace Sales 
{
    class Operations
    {
        public static void BuyProduct(in string name, in float price)
        {
            /* const */ List<InventoryItem> items = DataBase.GetItemService.GetItem(name);
            /* const */ InventoryItem item = HandleNewItem(items, name);
            /* const */ int quantity = item.Quantity;
            /* const */ float cost_basis = UpdateCostBasis(item.Cost_basis, quantity, price);
            DataBase.UpdateItemService.Update(name, quantity+1, cost_basis);
        }
        private static InventoryItem HandleNewItem(in List<InventoryItem> items, in string name)
        {
            if (items.Count == 0) {
                /* const */ InventoryItem newItem = new(name);
                DataBase.NewItemService.Create(newItem.Name, newItem.Quantity, newItem.Cost_basis);
                return newItem;
            }
            else {
                return items[0];
            }
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
                /* const */ List<InventoryItem> items = DataBase.GetItemService.GetItem(name);
                /* const */ InventoryItem item = HandleNonExistentProduct(items, name);
                /* const */ int quantity = item.Quantity;
                DataBase.UpdateItemService.Update(name, quantity, item.Cost_basis);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static InventoryItem HandleNonExistentProduct(in List<InventoryItem> items, in string name)
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
    }
}