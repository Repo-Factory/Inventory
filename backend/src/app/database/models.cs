namespace Models
{
    public class Inventory
    {
        public class InventoryItem
        {
            public required int Id { get; set; }
            public required string Name { get; set; }
            public required int Quantity { get; set; }
            public required float Cost_basis { get; set; }
            public required float Sell_price { get; set; }
        }

        public enum InventoryItemOptions
        {
            Id = 0,
            Name = 1,
            Quantity = 2,
            Cost_basis = 3,
            Sell_price = 4
        }

        public class Sale
        {
            public required string Name { get; set; }
            public required DateTime Timestamp { get; set; }
            public required float Sell_price { get; set; }
        }
        public enum SaleOptions
        {
            Name = 0,
            DateTime = 1,
            Sell_price = 2
        }
    }
}