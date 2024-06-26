using HotelWebApi.Data;

public class Db : DbContext
{
    public Db(DbContextOptions<Db> options) : base(options) { }

    //public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Определение связи многие-ко-многим между Book и Author
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books);
    }
}
