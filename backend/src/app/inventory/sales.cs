namespace Sales 
{
    class Operations
    {
        public static void BuyProduct(in string name, in float price)
        {
            string readQuery =  @$" SELECT quantity 
                                    FROM inventory 
                                    WHERE name=={name}";
            int quantity = DataBase.Query.Read(readQuery, Models.Inventory.InventoryItemOptions.Quantity);
            string writeQuery = @$" UPDATE inventory
                                    SET quantity={quantity+1}
                                    WHERE condition;";
            DataBase.Query.Update(writeQuery);
        }

        public static void SellProduct(in string name)
        {
            string readQuery =  @$" SELECT quantity 
                                    FROM inventory 
                                    WHERE name=={name}";
            int quantity = DataBase.Query.Read(readQuery, Models.Inventory.InventoryItemOptions.Quantity);
            string writeQuery = @$" UPDATE inventory
                                    SET quantity={quantity-1}
                                    WHERE condition;";
            DataBase.Query.Update(writeQuery);
        }
    }
}