
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HotelDb>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<HotelDb>();
    db.Database.EnsureCreated();
}

app.MapGet("/hotels", async (HotelDb db) => await db.Hotels.ToListAsync());

app.MapGet("/hotels/{id}", async (int id, HotelDb db) => 
await db.Hotels.FirstOrDefaultAsync(x => x.Id == id) is Hotel hotel 
? Results.Ok(hotel) 
: Results.NotFound());

app.MapPost("/hotels", async ([FromBody] Hotel hotel, [FromServices] HotelDb db, HttpResponse response) =>
{
    db.Hotels.Add(hotel);
    await db.SaveChangesAsync();
    response.StatusCode = 201;
    response.Headers.Location = $"/hotels/{hotel.Id}";
});

app.MapPut("/hotels", async (Hotel hotel, HotelDb db) =>
{

});
app.MapDelete("/hotels/{id}", (int id) =>
{

});

app.Run();

public class HotelDb : DbContext
{
    public HotelDb(DbContextOptions<HotelDb> options) : base(options) { }

    public DbSet<Hotel> Hotels => Set<Hotel>();


}

public class Hotel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}