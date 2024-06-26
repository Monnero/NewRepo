namespace HotelWebApi.Data
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Author> Authors { get; set; } = new();
    }
    public class BooksDb : DbContext
    {
        public BooksDb(DbContextOptions<BooksDb> options) : base(options) { }

        public DbSet<Books> Books => Set<Books>();
    }

}
