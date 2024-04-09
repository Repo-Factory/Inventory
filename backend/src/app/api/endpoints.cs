using System.Numerics;

namespace Endpoints
{
    public class Expose
    {
        public static void BuyProduct(WebApplication app)
        {
            const string dataEndpoint = "/buyProduct";
            const string endpointName = "BuyProduct";
            app.MapPost(dataEndpoint, (string name, float cost_basis) =>
            {
                try
                {
                    Sales.Operations.BuyProduct(name, cost_basis);
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    Console.Write($"An error occurred while processing the request {ex}.");
                    return Results.StatusCode(StatusCodes.Status500InternalServerError);
                }
            })
            .WithName(endpointName)
            .WithOpenApi();
        }

        public static void SellProduct(WebApplication app)
        {
            const string dataEndpoint = "/sellProduct";
            const string endpointName = "SellProduct";
            app.MapDelete(dataEndpoint, (string name) =>
            {
                try
                {
                    Sales.Operations.SellProduct(name);
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    Console.Write($"An error occurred while processing the request {ex}.");
                    return Results.StatusCode(StatusCodes.Status500InternalServerError);
                }
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

        public static void IntegerEndpoint(WebApplication app)
        {
            const string dataEndpoint = "/integer";
            const string endpointName = "/GetInteger";
            app.MapGet(dataEndpoint, (int number) =>
            {
                return Enumerable.Range(1, 5).Select(index =>
                    new BigInteger
                    (
                        number
                    ))
                    .ToArray();
            })
            .WithName(endpointName)
            .WithOpenApi();
        }
    }
}