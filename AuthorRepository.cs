
using HotelWebApi.Data;
using HotelWebApi.Interfaces;

namespace HotelWebApi
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly Db _context;
        public AuthorRepository(Db context) => _context = context;
        public async Task<List<Author>> GetAuthorsAsync()
        {
            return await (from a in _context.Authors
                          select new Author
                          {
                              Id = a.Id,
                              FirstName = a.FirstName,
                              LastName = a.LastName,
                              Language = a.Language,
                              IsMainAuthor = a.IsMainAuthor,
                              Books = a.Books
                          }).ToListAsync(); ;
        }
        public async Task<Author?> GetAuthorAsync(int authorId)
        {
            return await (from a in _context.Authors
                          where a.Id == authorId
                          select new Author
                          {
                              Id = a.Id,
                              FirstName = a.FirstName,
                              LastName = a.LastName,
                              Language = a.Language,
                              IsMainAuthor = a.IsMainAuthor,
                              Books = a.Books
                          }).FirstAsync();
        }

        public async Task InsertAuthorAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            var authorFromDb = await _context.Authors.FindAsync([author.Id]);
            if (authorFromDb is null) return;
            authorFromDb.FirstName = author.FirstName;
            authorFromDb.LastName = author.LastName;
            authorFromDb.IsMainAuthor = author.IsMainAuthor;

            authorFromDb.Books.AddRange(author.Books);


            authorFromDb.IsMainAuthor = author.IsMainAuthor;
            authorFromDb.Language = author.Language;
        }

        public async Task DeleteAuthorAsync(int authorId)
        {
            var authorFromDb = await _context.Authors.FindAsync([authorId]);
            if (authorFromDb is null) return;
            _context.Authors.Remove(authorFromDb);
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
