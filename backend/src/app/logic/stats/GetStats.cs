using Sale = Models.Inventory.Sale;
using Stats = Statistics.SalesStats;

namespace Logic 
{
    class GetStatsService
    {
        public static void GetStats(DateTime before, DateTime after)
        {
            /* const */ List<Sale> sales = DataBase.GetSalesService.GetSales(before, after);
            List<List<Sale>> groupedSales = GroupSales(sales);
            List<Stats> stats = TotalStats(groupedSales);
            DisplayStats(stats);
        }
        private static List<List<Sale>> GroupSales(in List<Sale> sales)
        {
            var groupedSales = new Dictionary<string, List<Sale>>();
            foreach (var sale in sales)
            {
                if (!groupedSales.TryGetValue(sale.Name, out List<Sale>? value))
                {
                    value = ([]);
                    groupedSales[sale.Name] = value;
                }
                value.Add(sale);
            }
            return [.. groupedSales.Values];
        }
        private static List<Stats> TotalStats(in List<List<Sale>> groupedSales)
        {
            List<Stats> total_stats = [];
            foreach (List<Sale> groupedSale in groupedSales)
            {
                List<Stats> stats = GroupedStats(groupedSale);
                total_stats.Add(Statistics.Totals.CalculateStats(stats));
            }
            return total_stats;
        }
        private static List<Stats> GroupedStats(in List<Sale> groupedSale)
        {
            List<Stats> stats = [];
            foreach (Sale sale in groupedSale)
            {
                float sell_price = sale.Sale_price;
                float buy_price = 0;
                Stats stat = new(sell_price, buy_price);
                stats.Add(stat);
            }
            return stats;
        }
        private static void DisplayStats(List<Stats> stats)
        {

        }
    }
}