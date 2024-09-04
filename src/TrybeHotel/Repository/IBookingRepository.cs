using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public interface IBookingRepository
    {
        BookingResponse Add(BookingDtoInsert booking, string userEmail);
        Room GetRoomById(int RoomId);
        BookingResponse GetBooking(int bookingId, string email);
        List<BookingResponse> GetBookings(string email);
        BookingResponse UpdateBooking(int bookingId, BookingDtoInsert booking, string email);
        void DeleteBooking(int bookingId);

    }
}