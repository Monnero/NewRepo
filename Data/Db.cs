using HotelWebApi.Data;

public class Db : DbContext
{
    public Db(DbContextOptions<Db> options) : base(options) { }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();
   
}
