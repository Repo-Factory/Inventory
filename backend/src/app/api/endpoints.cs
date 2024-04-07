using System.Numerics;

namespace Endpoints
{
    public class Expose
    {
        public static void IntegerEndpoint(WebApplication app)
        {
            const string dataEndpoint = "/integer";
            const string endpointName = "/GetInteger";
            app.MapGet(dataEndpoint, () =>
            {
                Random random = new();
                return Enumerable.Range(1, 5).Select(index =>
                    new BigInteger
                    (
                        random.Next(100)
                    ))
                    .ToArray();
            })
            .WithName(endpointName)
            .WithOpenApi();
        }
    }
}