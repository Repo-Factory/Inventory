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
                    Logic.BuyProductService.BuyProduct(name, cost_basis);
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
                    Logic.SellProductService.SellProduct(name);
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
    }
}