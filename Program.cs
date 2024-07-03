using HotelWebApi.Data;
using HotelWebApi.Interfaces;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Db>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
});
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });
builder.Services.AddMvc();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Êàòàëîã êíèã", Version = "v1" });
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<Db>();

    //db.Database.EnsureDeleted();
    //db.Database.EnsureCreated();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/order", async (IOrderRepository repository) => Results.Ok(await repository.GetOrdersAsync()));

app.MapGet("/order/{id}", async (int id, IOrderRepository repository) =>
await repository.GetOrderAsync(id) is Order order
? Results.Ok(order)
: Results.NotFound());


app.MapPost("/order", async ([FromBody] Order order, IOrderRepository repository) =>
{
    await repository.InsertOrderAsync(order);
    await repository.SaveAsync();
    return Results.Created($"/order/{order.Id}", order);
});

app.MapPut("/order", async ([FromBody] Order order, IOrderRepository repository) =>
{
    await repository.UpdateOrderAsync(order);
    await repository.SaveAsync();
    return Results.Created($"/order/{order.Id}", order);
});

app.MapDelete("/order/{id}", async (int id, IOrderRepository repository) =>
{
    await repository.DeleteOrderAsync(id);
    await repository.SaveAsync();
    return Results.Ok();
});



app.MapGet("/item", async (IItemRepository repository) => Results.Ok(await repository.GetItemsAsync()));

app.MapGet("/item/{id}", async (int id, IItemRepository repository) =>
await repository.GetItemAsync(id) is Item item
? Results.Ok(item)
: Results.NotFound());

app.MapPost("/item", async ([FromBody] Item item, IItemRepository repository) =>
{

    await repository.InsertItemAsync(item);
    await repository.SaveAsync();
    return Results.Created($"/author/{item.Id}", item);


});

app.MapPut("/item", async ([FromBody] Item item, IItemRepository repository) =>
{
    await repository.UpdateItemAsync(item);
    await repository.SaveAsync();
    return Results.Created($"/item/{item.Id}", item);



});

app.MapDelete("/item/{id}", async (int id, IItemRepository repository) =>
{
    await repository.DeleteItemAsync(id);
    await repository.SaveAsync();
    return Results.Ok();
});
#endregion
app.UseHttpsRedirection();
app.Run();
