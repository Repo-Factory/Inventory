using Sale = Models.Inventory.Sale;
using Earnings = Statistics.SalesStats;

namespace Logic 
{
    class Stats(in int quantity_sold, in int quantity_left, in Earnings earnings)
    {
        public int quantity_sold = quantity_sold;
        public int quantity_left = quantity_left;
        public Earnings earnings = earnings;
    } 
    public class GetStatsService
    {
        public static void GetStats(in DateTime before, in DateTime after)
        {
            /* const */ List<Sale> sales = DataBase.GetSalesService.GetSales(before, after);
            /* const */ List<List<Sale>> groupedSales = GroupSalesByName(sales);
            /* const */ Dictionary<string, Stats> stats = CalculateStatsForAllGroups(groupedSales);
            /* const */ Earnings totals = CalculateTotals(stats);
            DisplayStats(stats, before, after);
            DisplayTotals(totals);
        }
        private static List<List<Sale>> GroupSalesByName(in List<Sale> sales)
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
        private static Dictionary<string, Stats> CalculateStatsForAllGroups(in List<List<Sale>> groupedSales)
        {
            Dictionary<string, Stats> itemBreakdown = [];
            foreach (List<Sale> groupedSale in groupedSales)
            {
                string product_name = groupedSale[0].Name;
                List<Earnings> earnings = GetListOfStatsForGroup(groupedSale);
                int quantity_sold = GetQuantitySold(groupedSale);
                int quantity_left = DataBase.GetItemService.GetItem(product_name)[0].Quantity;
                Stats stats = new(quantity_sold, quantity_left, Statistics.Totals.CalculateStats(earnings));
                itemBreakdown.Add(product_name, stats);
            }
            return itemBreakdown;
        }
        private static int GetQuantitySold(in List<Sale> groupedSale)
        {
            int i = 0;
            foreach (Sale sale in groupedSale)
            {
                i++;
            }
            return i;
        }
        private static List<Earnings> GetListOfStatsForGroup(in List<Sale> groupedSale)
        {
            List<Earnings> stats = [];
            Dictionary<string, float> costs = GetCostsService.GetCosts();
            foreach (Sale sale in groupedSale)
            {
                float sell_price = sale.Sale_price;
                float buy_price = costs[sale.Name];
                Earnings stat = new(sell_price, buy_price);
                stats.Add(stat);
            }
            return stats;
        }
        private static Earnings CalculateTotals(in Dictionary<string, Stats> named_stats)
        {
            List<Earnings> stats = [];
            foreach (var stat in named_stats)
            {
                stats.Add(stat.Value.earnings);
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
                Console.WriteLine($"Con el producto {name} vendiste {stat.quantity_sold} - te quedan {stat.quantity_left}");
                Console.WriteLine($"----------------------------");
                Console.WriteLine($"Se ingresaron {stat.earnings.revenue} pesos");
                Console.WriteLine($"Ganaste {stat.earnings.profit} pesos");
                Console.WriteLine($"Tu margen promedio sale a {stat.earnings.margin:P1}");
                Console.WriteLine($"----------------------------");
                Console.WriteLine();
            }
        }
        private static void DisplayTotals(in Earnings totals)
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