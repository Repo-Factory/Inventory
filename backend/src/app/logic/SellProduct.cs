using InventoryItem = Models.Inventory.Item;

namespace Logic 
{
    class SellProductService
    {
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