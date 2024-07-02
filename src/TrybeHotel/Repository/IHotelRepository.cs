using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public interface IHotelRepository
    {
        IEnumerable<HotelDto> GetHotels();
        HotelDto AddHotel(Hotel hotel);
        void DeleteHotel(int id);
        HotelDto UpdateHotel(Hotel hotel);
    }
}