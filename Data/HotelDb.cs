public class HotelDb : DbContext
{
    public HotelDb(DbContextOptions<HotelDb> options) : base(options) { }

    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<Books> Books => Set<Books>();
    public DbSet<Author> Author => Set<Author>();

}
