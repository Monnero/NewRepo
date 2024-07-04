
using HotelWebApi.Data;
using HotelWebApi.Interfaces;

namespace HotelWebApi
{
    public class ItemRepository : IItemRepository
    {
        private readonly Db _context;
        public ItemRepository(Db context) => _context = context;
        public async Task<List<Item>> GetItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }
        public async Task<Item?> GetItemAsync(int itemId)
        {
            return await _context.Items.FindAsync(itemId);
        }

        public async Task InsertItemAsync(Item item)
        {
            await _context.Items.AddAsync(item);
        }

        public async Task UpdateItemAsync(Item item)
        {
            var itemFromDb = await _context.Items.FindAsync([item.Id]);
            if (itemFromDb is null) return;
            itemFromDb.Name = item.Name;
            itemFromDb.Price = item.Price;
            itemFromDb.OrderId = item.OrderId;
            itemFromDb.Order = item.Order;
        }

        public async Task DeleteItemAsync(int itemId)
        {
            var itemFromDb = await _context.Items.FindAsync([itemId]);
            if (itemFromDb is null) return;
            _context.Items.Remove(itemFromDb);
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
