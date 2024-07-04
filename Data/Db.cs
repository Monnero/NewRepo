using HotelWebApi.Data;

public class Db : DbContext
{
    public Db(DbContextOptions<Db> options) : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Item> Items => Set<Item>();
   
}
