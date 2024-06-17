using TrybeHotel.Models;
using TrybeHotel.Dto;
using System.Xml.Schema;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<HotelDto> GetHotels()
        {
            var hotels = from hotel in _context.Hotels
                         select new HotelDto
                         {
                             HotelId = hotel.HotelId,
                             Name = hotel.Name,
                             Address = hotel.Address,
                             CityId = hotel.CityId,
                             CityName = hotel.City!.Name,
                             State = hotel.City!.State
                         };
            return hotels.ToList();
        }
        
        public HotelDto AddHotel(Hotel hotel)
        {
            try
            {
                _context.Hotels.Add(hotel);
                _context.SaveChanges();
                var newHotel = _context.Hotels.First(h => h.HotelId == hotel.HotelId);
                newHotel.City = _context.Cities.First(c => c.CityId == hotel.CityId);
                return new HotelDto
                {
                    HotelId = newHotel.HotelId,
                    Name = newHotel.Name,
                    Address = newHotel.Address,
                    CityId = newHotel.CityId,
                    CityName = newHotel.City.Name,
                    State = newHotel.City.State
                };
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}