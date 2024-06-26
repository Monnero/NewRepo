
using HotelWebApi.Data;
using HotelWebApi.Interfaces;

namespace HotelWebApi
{
    public class BookRepository : IBookRepository
    {
        private readonly Db _context;
        public BookRepository(Db context) => _context = context;
        public async Task<List<Book>> GetBooksAsync()
        {
            return await _context.Books.ToListAsync();
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

        public Task<Hotel?> GetHotelAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task InsertHotelAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task UpdateHotelAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task DeleteHotelAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<Book?> GetBookAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task InsertBookAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBookAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBookAsync(int bookId)
        {
            throw new NotImplementedException();
        }
    }
}
