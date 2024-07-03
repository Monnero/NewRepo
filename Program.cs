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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Каталог книг", Version = "v1" });
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
#region Методы для работы с книгами
///<summary> Метод для получения всех книг </summary>
app.MapGet("/order", async (IOrderRepository repository) => Results.Ok(await repository.GetOrdersAsync()));

///<summary> Метод для получения книги по id </summary>
app.MapGet("/order/{id}", async (int id, IOrderRepository repository) =>
await repository.GetOrderAsync(id) is Order order
? Results.Ok(order)
: Results.NotFound());


///<summary> Метод для добавления книги </summary>
app.MapPost("/order", async ([FromBody] Order order, IOrderRepository repository) =>
{
    await repository.InsertOrderAsync(order);
    await repository.SaveAsync();
    return Results.Created($"/order/{order.Id}", order);
});

///<summary> Метод для обновления книги </summary>
app.MapPut("/order", async ([FromBody] Order order, IOrderRepository repository) =>
{
    await repository.UpdateOrderAsync(order);
    await repository.SaveAsync();
    return Results.Created($"/order/{order.Id}", order);
});

///<summary> Метод для удаления книги </summary>
app.MapDelete("/order/{id}", async (int id, IOrderRepository repository) =>
{
    await repository.DeleteOrderAsync(id);
    await repository.SaveAsync();
    return Results.Ok();
});

#endregion

#region Методы для работы с авторами

///<summary> Метод для получения всех items </summary>
app.MapGet("/item", async (IItemRepository repository) => Results.Ok(await repository.GetItemsAsync()));

///<summary> Метод для получения автора по id </summary>
app.MapGet("/item/{id}", async (int id, IItemRepository repository) =>
await repository.GetItemAsync(id) is Item item
? Results.Ok(item)
: Results.NotFound());

///<summary> Метод для добавления item </summary>
app.MapPost("/item", async ([FromBody] Item item, IItemRepository repository) =>
{

    await repository.InsertItemAsync(item);
    await repository.SaveAsync();
    return Results.Created($"/author/{item.Id}", item);


});

///<summary> Метод для обновления item </summary>
app.MapPut("/item", async ([FromBody] Item item, IItemRepository repository) =>
{
    await repository.UpdateItemAsync(item);
    await repository.SaveAsync();
    return Results.Created($"/item/{item.Id}", item);



});

///<summary> Метод для удаления item </summary>
app.MapDelete("/item/{id}", async (int id, IItemRepository repository) =>
{
    await repository.DeleteItemAsync(id);
    await repository.SaveAsync();
    return Results.Ok();
});
#endregion
app.UseHttpsRedirection();
app.Run();
