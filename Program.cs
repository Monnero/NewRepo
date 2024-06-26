using HotelWebApi.Data;
using HotelWebApi.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Db>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
});
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<Db>();

    //db.Database.EnsureDeleted();
   // db.Database.EnsureCreated();

    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapGet("/hotels", async (IHotelRepository repository) => Results.Ok(await repository.GetHotelsAsync()));
app.MapGet("/books", async (IBookRepository repository) => Results.Ok(await repository.GetBooksAsync()));
app.MapGet("/author", async (IBookRepository repository) => Results.Ok(await repository.GetBooksAsync()));

//app.MapGet("/hotels/{id}", async (int id, IHotelRepository repository) =>
//await repository.GetHotelAsync(id) is Hotel hotel
//? Results.Ok(hotel)
//: Results.NotFound());

app.MapPost("/books", async ([FromBody] Book book, IBookRepository repository) =>
{
    await repository.InsertBookAsync(book);
    await repository.SaveAsync();
    return Results.Created($"/books/{book.Id}", book);
});

app.MapPut("/books", async ([FromBody] Book book, IBookRepository repository) =>
{
    await repository.UpdateBookAsync(book);
    await repository.SaveAsync();
    return Results.NoContent();
});
app.MapDelete("/books/{id}", async (int id, IBookRepository repository) =>
{
    await repository.DeleteBookAsync(id);
    await repository.SaveAsync();
    return Results.NoContent();
});

app.UseHttpsRedirection();
app.Run();
