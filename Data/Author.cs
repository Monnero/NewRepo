namespace HotelWebApi.Data
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsMainAuthor { get; set; }
    }
    public class AuthorDb : DbContext
    {
        public AuthorDb(DbContextOptions<AuthorDb> options) : base(options) { }

        public DbSet<Author> Books => Set<Author>();
    }
}
