
namespace HotelWebApi
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelDb _context;
        public HotelRepository(HotelDb context) => _context = context;
        public async Task<List<Hotel>> GetHotelsAsync()
        {
            return await _context.Hotels.ToListAsync();
        }
        public async Task<Hotel?> GetHotelAsync(int hotelId)
        {
            return await _context.Hotels.FindAsync([hotelId]);
        }
        public async Task InsertHotelAsync(Hotel hotel)
        {
            await _context.Hotels.AddAsync(hotel);
        }
        public async Task UpdateHotelAsync(Hotel hotel)
        {
            var hotelFromDb = await _context.Hotels.FindAsync([hotel.Id]);
            if (hotelFromDb is null) return;
            hotelFromDb.Name = hotel.Name;
            hotelFromDb.Longitude = hotel.Longitude;
            hotelFromDb.Latitude = hotel.Latitude;

        }
        public async Task DeleteHotelAsync(int hotelId)
        {
            var hotelFromDb = await _context.Hotels.FindAsync([hotelId]);
            if (hotelFromDb is null) return;
            _context.Hotels.Remove(hotelFromDb);
        }

        public async Task SaveAsync()=> await _context.SaveChangesAsync();

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
