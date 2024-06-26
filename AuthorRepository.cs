
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
            return await _context.Authors.ToListAsync();
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

        public Task<Author?> GetAuthorAsync(int AuthorId)
        {
            throw new NotImplementedException();
        }

        public Task InsertAuthorAsync(Author Author)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAuthorAsync(Author Author)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAuthorAsync(int AuthorId)
        {
            throw new NotImplementedException();
        }

    }
}
