using EfOrders.Api.Core.DomainServices;
using EfOrders.Api.Data;
using EfOrders.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("OrdersDb");
builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();
builder.Services.AddScoped<OrdersDbContext>();
var app = builder.Build();
app.UseHttpsRedirection();
app.MapGet("/orders", async (IOrderRepository repository) =>
{
    var orders =  await repository.GetAllAsync();
    return Results.Ok(orders);
});

app.MapGet("/orders/{id:int}", async (int id, IOrderRepository repository) =>
{
    var order = await repository.FindAsync(id);
    return order == null
        ? Results.NotFound()
        : Results.Ok(order);
});

app.Run();
