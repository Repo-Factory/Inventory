namespace Models
{
    public class Inventory
    {
        public class Item(string name, int quantity = 0, float cost_basis = 0.00001f)
        {
            public string Name { get; set; } = name;
            public int Quantity { get; set; } = quantity;
            public float Cost_basis { get; set; } = cost_basis;
        }
        public class Sale(string name, DateTime timestamp, float sell_price)
        {
            public string Name { get; set; } = name;
            public DateTime Timestamp { get; set; } = timestamp;
            public float Sell_price { get; set; } = sell_price;
        }

    }
}