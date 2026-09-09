using EfOrders.Api.Core.DomainServices;
using EfOrders.Api.Infrastructure;
using EfOrders.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();
builder.Services.AddDbContext<OrdersDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("OrdersDb"))
            .LogTo(
                Console.WriteLine,
                new[]
                {
                    DbLoggerCategory
                        .Database
                        .Command
                        .Name
                },
                LogLevel.Information)
        );
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
app.MapGet("/demo1", async (IOrderRepository repository) =>
{
    return await repository.GetOrdersAboveTotal1000();
});

app.MapGet("/demo2", async (IOrderRepository repository) =>
{
    return await repository.GetCustomers();
});

app.Run();
