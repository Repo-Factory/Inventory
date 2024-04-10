namespace Statistics
{
    public class Totals
    {
        public static SalesStats CalculateStats(in List<SalesStats> sales)
        {
            float total_profit = 0;
            float total_revenue = 0;
            for (int i = 0; i < sales.Count; i++)
            {
                total_profit += sales[i].profit;
                total_revenue += sales[i].revenue;
            }
            float total_margin = total_revenue/total_profit;
            return new SalesStats(total_revenue, total_profit, total_margin);
        }
    }
}