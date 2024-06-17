using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public interface IBookingRepository
    {
        BookingResponse Add(BookingDtoInsert booking);
        Room GetRoomById(int RoomId);
        BookingResponse GetBooking(int bookingId, string email);
    }
}