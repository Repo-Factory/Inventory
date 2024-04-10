namespace Statistics
{
    public class SalesStats
    {
        public SalesStats(){}
        public SalesStats(in float sell_price, in float cost_basis)
        {
            revenue = CalculateRevenue(sell_price);
            profit = CalculateProfit(sell_price, cost_basis);
            margin = CalculateMargin(revenue, profit);
        }
        public SalesStats(in float revenue, in float profit, in float margin)
        {
            this.revenue = revenue;
            this.profit = profit;
            this.margin = margin;
        }
        public float revenue;
        public float profit;
        public float margin;

        public static float CalculateRevenue(in float sell_price)
        {
            return sell_price;
        }
        public static float CalculateProfit(in float sell_price, in float cost_basis)
        {
            return sell_price - cost_basis;
        }

        public static float CalculateMargin(in float revenue, in float profit)
        {
            if (revenue == 0)
            {
                throw new ArgumentException("Revenue cannot be zero.");
            }
            return profit / revenue * 100;
        }
    }
}