using HotelWebApi.Data;
using HotelWebApi.Interfaces;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Db>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
});
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
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

//app.MapGet("/hotels", async (IHotelRepository repository) => Results.Ok(await repository.GetHotelsAsync()));
app.MapGet("/books", async (IBookRepository repository) => Results.Ok(await repository.GetBooksAsync()));
app.MapGet("/author", async (IAuthorRepository repository) => Results.Ok(await repository.GetAuthorsAsync()));

app.MapGet("/books/{id}", async (int id, IBookRepository repository) =>
await repository.GetBookAsync(id) is Book book
? Results.Ok(book)
: Results.NotFound());
app.MapGet("/author/{id}", async (int id, IAuthorRepository repository) =>
await repository.GetAuthorAsync(id) is Author author
? Results.Ok(author)
: Results.NotFound());

app.MapPost("/books", async ([FromBody] Book book, IBookRepository repository) =>
{
    await repository.InsertBookAsync(book);
    await repository.SaveAsync();
    return Results.Created($"/books/{book.Id}", book);
});
app.MapPost("/author", async ([FromBody] Author author, IAuthorRepository repository) =>
{
    if (author.IsValidate())
    {
        await repository.InsertAuthorAsync(author);
        await repository.SaveAsync();
        return Results.Created($"/author/{author.Id}", author);

    }
    else return Results.Problem("author is not validate");
});

app.MapPut("/books", async ([FromBody] Book book, IBookRepository repository) =>
{
    await repository.UpdateBookAsync(book);
    await repository.SaveAsync();
    return Results.NoContent();
});
app.MapPut("/author", async ([FromBody] Author author, IAuthorRepository repository) =>
{
    if (author.IsValidate())
    {
        await repository.UpdateAuthorAsync(author);
        await repository.SaveAsync();
        return Results.NoContent();

    }
    else return Results.Problem("author is not validate");

});

app.MapDelete("/books/{id}", async (int id, IBookRepository repository) =>
{
    await repository.DeleteBookAsync(id);
    await repository.SaveAsync();
    return Results.NoContent();
});
app.MapDelete("/author/{id}", async (int id, IAuthorRepository repository) =>
{
    await repository.DeleteAuthorAsync(id);
    await repository.SaveAsync();
    return Results.NoContent();
});

app.UseHttpsRedirection();
app.Run();
