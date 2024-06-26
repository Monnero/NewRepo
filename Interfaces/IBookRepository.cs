using HotelWebApi.Data;

namespace HotelWebApi.Interfaces
{
    interface IBookRepository : IDisposable
    {
        Task<List<Book>> GetBooksAsync();
        Task<Book?> GetBookAsync(int bookId);
        Task InsertBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int bookId);
        Task SaveAsync();
    }
}
