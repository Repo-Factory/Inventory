using System.Data;
using Sale = Models.Inventory.Sale;
using Stats = Statistics.SalesStats;

namespace Logic 
{
    class GetStatsService
    {
        public static void GetStats(in DateTime before, in DateTime after)
        {
            /* const */ List<Sale> sales = DataBase.GetSalesService.GetSales(before, after);
            List<List<Sale>> groupedSales = GroupSales(sales);
            Dictionary<string, Stats> stats = CalculateStatsForGroup(groupedSales);
            DisplayStats(stats, before, after);
            Stats totals = CalculateTotals(stats);
            DisplayTotals(totals, before, after);
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
        private static Dictionary<string, Stats> CalculateStatsForGroup(in List<List<Sale>> groupedSales)
        {
            Dictionary<string, Stats> itemBreakdown = [];
            foreach (List<Sale> groupedSale in groupedSales)
            {
                List<Stats> stats = GetListOfStatsForGroup(groupedSale);
                itemBreakdown.Add(groupedSale[0].Name, Statistics.Totals.CalculateStats(stats));
            }
            return itemBreakdown;
        }
        private static List<Stats> GetListOfStatsForGroup(in List<Sale> groupedSale)
        {
            List<Stats> stats = [];
            Dictionary<string, float> costs = GetCostsService.GetCosts();
            foreach (Sale sale in groupedSale)
            {
                float sell_price = sale.Sale_price;
                float buy_price = costs[sale.Name];
                Stats stat = new(sell_price, buy_price);
                stats.Add(stat);
            }
            return stats;
        }
        private static Stats CalculateTotals(in Dictionary<string, Stats> named_stats)
        {
            List<Stats> stats = [];
            foreach (var stat in named_stats)
            {
                stats.Add(stat.Value);
            }
            return Statistics.Totals.CalculateStats(stats);
        }
        private static void DisplayStats(in Dictionary<string, Stats> stats, in DateTime before, in DateTime after)
        {
            Console.WriteLine(); Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
            Console.WriteLine($"Por el periodo que se encuentra entre {after} y {before}");
            Console.WriteLine($"****************************************************"); Console.WriteLine();
            foreach (var (name, stat) in stats)
            {
                Console.WriteLine($"Con el producto {name} pesos");
                Console.WriteLine($"----------------------------");
                Console.WriteLine($"Vendiste {stat.revenue} pesos");
                Console.WriteLine($"Ganaste {stat.profit} pesos");
                Console.WriteLine($"Tu margen promedio sale a {stat.margin:P1}");
                Console.WriteLine($"----------------------------");
                Console.WriteLine();
            }
        }
        private static void DisplayTotals(in Stats totals, in DateTime before, in DateTime after)
        {
            Console.WriteLine($"************************ TOTALES ****************************");
            
                Console.WriteLine($"Vendiste {totals.revenue} pesos");
                Console.WriteLine($"Ganaste {totals.profit} pesos");
                Console.WriteLine($"Tu margen promedio sale a {totals.margin:P1}");
            
            Console.WriteLine($"************************ ******* ****************************");
            Console.WriteLine(); Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
        }
    }
}