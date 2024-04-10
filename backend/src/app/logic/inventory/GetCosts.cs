using InventoryItem = Models.Inventory.Item;

namespace Logic 
{
    class GetCostsService
    {
        public static Dictionary<string, float> GetCosts()
        {
            Dictionary<string, float> costs = [];
            /* const */ List<InventoryItem> items = DataBase.GetInventoryService.GetInventory();
            foreach(var item in items)
            {
                costs.Add(item.Name, item.Cost_basis);
            }
            return costs;
        }
    }
}