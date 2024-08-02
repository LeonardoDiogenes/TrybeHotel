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
                             Image = hotel.Image,
                             Name = hotel.Name,
                             Address = hotel.Address,
                             CityId = hotel.CityId,
                             CityName = hotel.City!.Name,
                             State = hotel.City!.State
                         };
            var hotelsList = hotels.ToList();
            if (hotelsList.Count == 0)
                throw new Exception("No hotels found");
            return hotelsList;
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
                    Image = newHotel.Image,
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

        public void DeleteHotel(int id)
        {
            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == id);
            if (hotel == null)
                throw new Exception("Hotel not found");

            _context.Hotels.Remove(hotel);
            _context.SaveChanges();
        }

        public HotelDto UpdateHotel(Hotel hotel)
        {
            var hotelToUpdate = _context.Hotels.FirstOrDefault(h => h.HotelId == hotel.HotelId);
            if (hotelToUpdate == null)
                throw new Exception("Hotel not found");
            hotelToUpdate.City = _context.Cities.FirstOrDefault(c => c.CityId == hotel.CityId);
            if (hotelToUpdate.City == null)
                throw new Exception("Hotel not found");

            hotelToUpdate.Name = hotel.Name;
            hotelToUpdate.Address = hotel.Address;
            hotelToUpdate.CityId = hotel.CityId;
            hotelToUpdate.Image = hotel.Image;
            _context.SaveChanges();

            return new HotelDto
            {
                HotelId = hotelToUpdate.HotelId,
                Image = hotelToUpdate.Image,
                Name = hotelToUpdate.Name,
                Address = hotelToUpdate.Address,
                CityId = hotelToUpdate.CityId,
                CityName = hotelToUpdate.City!.Name,
                State = hotelToUpdate.City!.State
            };
        }
    }
}