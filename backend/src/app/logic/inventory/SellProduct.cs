using InventoryItem = Models.Inventory.Item;

namespace Logic 
{
    class SellProductService
    {
        public static void SellProduct(in string name, in float sale_price)
        {
            try
            {
                /* const */ List<InventoryItem> items = DataBase.GetItemService.GetItem(name);
                /* const */ InventoryItem item = HandleNonExistentProduct(items, name);
                /* const */ int quantity = item.Quantity;
                if (quantity == 0) {throw new ItemOutOfStockException();}
                DataBase.UpdateItemService.Update(name, quantity-1, item.Cost_basis);
                DataBase.AddSaleService.AddSale(name, DateTime.Now, sale_price);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ItemOutOfStockException ex)
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

        public class ItemOutOfStockException : Exception
        {
            public ItemOutOfStockException() : base("Can't sell an out of stock product") { }

            public ItemOutOfStockException(string message) : base(message) { }

            public ItemOutOfStockException(string message, Exception innerException) : base(message, innerException) { }
        }
    }
}