using HotelWebApi.Data;

namespace HotelWebApi
{
    interface IBookRepository : IDisposable
    {
        Task<List<Books>> GetBooksAsync();
        //Task<Hotel?> GetHotelAsync(int bookId);
        //Task InsertHotelAsync(Books book);
        //Task UpdateHotelAsync(Books book);
        //Task DeleteHotelAsync(int bookId);
        Task SaveAsync();
    }
}
