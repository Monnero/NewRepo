
using HotelWebApi.Data;
using HotelWebApi.Interfaces;
using System.Net;

namespace HotelWebApi
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Db _context;
        public OrderRepository(Db context) => _context = context;
        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<Order?> GetOrderAsync(int orderId)
        {            
            return await _context.Orders.FindAsync(orderId);
        }

        public async Task InsertOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var orderFromDb = await _context.Orders.FindAsync([order.Id]);
            if (orderFromDb is null) return;
            orderFromDb.Name = order.Name;
            orderFromDb.Items.AddRange(order.Items);
            orderFromDb.Description = order.Description;
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var orderFromDb = await _context.Orders.FindAsync([orderId]);
            if (orderFromDb is null) return;
            _context.Orders.Remove(orderFromDb);
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
