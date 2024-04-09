namespace Endpoints
{
    public class Expose
    {
        public static void BuyProduct(WebApplication app)
        {
            const string dataEndpoint = "/buyProduct";
            const string endpointName = "BuyProduct";
            app.MapGet(dataEndpoint, (in string name) =>
            {
                Sales.Operations.BuyProduct(name, 0);
            })
            .WithName(endpointName)
            .WithOpenApi();
        }

        public static void SellProduct(WebApplication app)
        {
            const string dataEndpoint = "/sellProduct";
            const string endpointName = "SellProduct";
            app.MapGet(dataEndpoint, (in string name) =>
            {
                Sales.Operations.SellProduct(name);
            })
            .WithName(endpointName)
            .WithOpenApi();
        }

        public static void GetStatistics(WebApplication app)
        {
            const string dataEndpoint = "/getStats";
            const string endpointName = "getStats";
            app.MapGet($"{dataEndpoint}/revenue", () =>
            {

            })
            .WithName($"{endpointName}/revenue")
            .WithOpenApi();

            app.MapGet($"{dataEndpoint}/margin", () =>
            {
                
            })
            .WithName($"{endpointName}/margin")
            .WithOpenApi();
        }

    }
}