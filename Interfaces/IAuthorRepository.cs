using HotelWebApi.Data;

namespace HotelWebApi.Interfaces
{
    interface IAuthorRepository : IDisposable
    {
        Task<List<Author>> GetAuthorsAsync();
        Task<Author?> GetAuthorAsync(int authorId);
        Task InsertAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int authorId);
        Task SaveAsync();
    }
}
