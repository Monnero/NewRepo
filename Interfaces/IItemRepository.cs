using HotelWebApi.Data;

namespace HotelWebApi.Interfaces
{
    interface IItemRepository : IDisposable
    {
        Task<List<Item>> GetItemsAsync();
        Task<Item?> GetItemAsync(int itemId);
        Task InsertItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(int itemId);
        Task SaveAsync();
    }
}
