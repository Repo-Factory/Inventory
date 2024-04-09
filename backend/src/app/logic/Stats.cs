namespace Statistics
{
    public class Product
    {
        public static float CalculateProfit(in float sell_price, in float buy_price)
        {
            return sell_price - buy_price;
        }
    
        public static float CalculateMargin(in float sell_price, in float buy_price)
        {
            return sell_price/buy_price - 1;
        }
    }
    public class Totals
    {
        public static float CalculateProfit(in float[] sell_price, in float[] cost_basis)
        {
            float profit = 0;
            for (int i = 0; i < sell_price.Length; i++)
            {
                profit += sell_price[i] - cost_basis[i];
            }
            return profit;
        }
    }
}