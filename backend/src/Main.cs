WebApplication setupApp(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    return builder.Build();
}

void setupHttpPipeline(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
} 

void setupEndpoints(WebApplication app)
{
    Endpoints.Expose.BuyProduct(app);
    Endpoints.Expose.SellProduct(app);
}

void startServer(string[] args)
{
    var app = setupApp(args);
    setupHttpPipeline(app);
    setupEndpoints(app);
    app.Run();
}

startServer([]);