using HotelWebApi.Data;
using HotelWebApi.Interfaces;
using Microsoft.OpenApi.Models;
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
app.MapGet("/books", async (IBookRepository repository) => Results.Ok(await repository.GetBooksAsync()));

///<summary> Метод для получения книги по id </summary>
app.MapGet("/books/{id}", async (int id, IBookRepository repository) =>
await repository.GetBookAsync(id) is Book book
? Results.Ok(book)
: Results.NotFound());


///<summary> Метод для добавления книги </summary>
app.MapPost("/books", async ([FromBody] Book book, IBookRepository repository) =>
{
    await repository.InsertBookAsync(book);
    await repository.SaveAsync();
    return Results.Created($"/books/{book.Id}", book);
});

///<summary> Метод для обновления книги </summary>
app.MapPut("/books", async ([FromBody] Book book, IBookRepository repository) =>
{
    await repository.UpdateBookAsync(book);
    await repository.SaveAsync();
    return Results.Created($"/books/{book.Id}", book);
});

///<summary> Метод для удаления книги </summary>
app.MapDelete("/books/{id}", async (int id, IBookRepository repository) =>
{
    await repository.DeleteBookAsync(id);
    await repository.SaveAsync();
    return Results.Ok();
});

#endregion

#region Методы для работы с авторами

///<summary> Метод для получения всех авторов</summary>
app.MapGet("/authors", async (IAuthorRepository repository) => Results.Ok(await repository.GetAuthorsAsync()));

///<summary> Метод для получения автора по id </summary>
app.MapGet("/author/{id}", async (int id, IAuthorRepository repository) =>
await repository.GetAuthorAsync(id) is Author author
? Results.Ok(author)
: Results.NotFound());

///<summary> Метод для добавления автора </summary>
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

///<summary> Метод для обновления автора </summary>
app.MapPut("/author", async ([FromBody] Author author, IAuthorRepository repository) =>
{
    if (author.IsValidate())
    {
        await repository.UpdateAuthorAsync(author);
        await repository.SaveAsync();
        return Results.Created($"/author/{author.Id}", author);

    }
    else return Results.Problem("author is not validate");

});

///<summary> Метод для удаления автора </summary>
app.MapDelete("/author/{id}", async (int id, IAuthorRepository repository) =>
{
    await repository.DeleteAuthorAsync(id);
    await repository.SaveAsync();
    return Results.Ok();
});
#endregion
app.UseHttpsRedirection();
app.Run();
