
using HotelWebApi.Data;
using HotelWebApi.Interfaces;
using System.Net;

namespace HotelWebApi
{
    public class BookRepository : IBookRepository
    {
        private readonly Db _context;
        public BookRepository(Db context) => _context = context;
        public async Task<List<Book>> GetBooksAsync()
        {
            return await (from b in _context.Books
                          select new Book
                          {
                              Id = b.Id,
                              Authors = b.Authors,
                              Description = b.Description,
                              Title = b.Title
                          }).ToListAsync();
        }
        public async Task<Book?> GetBookAsync(int bookId)
        {            
            return await (from b in _context.Books
                          where b.Id == bookId
                          select new Book
                          {
                              Id = b.Id,
                              Authors = b.Authors,
                              Description = b.Description,
                              Title = b.Title
                          }).FirstAsync();
        }

        public async Task InsertBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            var hotelFromDb = await _context.Books.FindAsync([book.Id]);
            if (hotelFromDb is null) return;
            hotelFromDb.Title = book.Title;
            hotelFromDb.Authors = book.Authors;
            hotelFromDb.Description = book.Description;
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var hotelFromDb = await _context.Books.FindAsync([bookId]);
            if (hotelFromDb is null) return;
            _context.Books.Remove(hotelFromDb);
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
