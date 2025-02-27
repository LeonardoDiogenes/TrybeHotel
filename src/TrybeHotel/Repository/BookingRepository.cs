using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public BookingResponse Add(BookingDtoInsert booking, string userEmail)
        {
            var room = _context.Rooms.First(r => r.RoomId == booking.RoomId);
            var user = _context.Users.First(u => u.Email == userEmail);
            if (booking.GuestQuant > room.Capacity)
            {
                throw new InvalidOperationException("Guest quantity over room capacity");
            }

            var newBooking = new Booking
            {
                CheckIn = booking.Checkin,
                CheckOut = booking.Checkout,
                GuestQuant = booking.GuestQuant,
                RoomId = booking.RoomId,
                UserId = user.UserId
            };
            _context.Bookings.Add(newBooking);
            _context.SaveChanges();

            var hotel = _context.Hotels.First(h => h.HotelId == room.HotelId);
            var city = _context.Cities.First(c => c.CityId == hotel.CityId);
            return new BookingResponse
            {
                GuestName = user.Name,
                BookingId = newBooking.BookingId,
                Checkin = newBooking.CheckIn,
                Checkout = newBooking.CheckOut,
                GuestQuant = newBooking.GuestQuant,
                Room = new RoomDto
                {
                    RoomId = room.RoomId,
                    Name = room.Name,
                    Capacity = room.Capacity,
                    Image = room.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = hotel.HotelId,
                        Name = hotel.Name,
                        Address = hotel.Address,
                        CityId = city.CityId,
                        CityName = city.Name,
                        State = city.State
                    }
                }
            };
        }

        public BookingResponse GetBooking(int bookingId, string email)
        {
            var guest = _context.Users.First(g => g.Email == email);
            var booking = _context.Bookings.First(b => b.BookingId == bookingId);
            if (guest.UserId != booking.UserId)
            {
                return null!;
            }
            
            var room = _context.Rooms.First(r => r.RoomId == booking.RoomId);
            var hotel = _context.Hotels.First(h => h.HotelId == room.HotelId);
            var city = _context.Cities.First(c => c.CityId == hotel.CityId);
            return new BookingResponse
            {
                GuestName = guest.Name,
                BookingId = booking.BookingId,
                Checkin = booking.CheckIn,
                Checkout = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                Room = new RoomDto
                {
                    RoomId = room.RoomId,
                    Name = room.Name,
                    Capacity = room.Capacity,
                    Image = room.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = hotel.HotelId,
                        Name = hotel.Name,
                        Address = hotel.Address,
                        CityId = city.CityId,
                        CityName = city.Name,
                        State = city.State
                    }
                }
            };
        }

        public Room GetRoomById(int RoomId)
        {
            throw new NotImplementedException();
        }

        public BookingResponse UpdateBooking(int bookingId, BookingDtoInsert booking, string userEmail)
        {
            var room = _context.Rooms.First(r => r.RoomId == booking.RoomId);
            var user = _context.Users.First(u => u.Email == userEmail);
            if (booking.GuestQuant > room.Capacity)
            {
                throw new InvalidOperationException("Guest quantity over room capacity");
            }

            var bookingToUpdate = _context.Bookings.First(b => b.BookingId == bookingId);
            if (bookingToUpdate.UserId != user.UserId)
            {
                throw new InvalidOperationException("User not allowed to update this booking");
            }

            bookingToUpdate.CheckIn = booking.Checkin;
            bookingToUpdate.CheckOut = booking.Checkout;
            bookingToUpdate.GuestQuant = booking.GuestQuant;
            bookingToUpdate.RoomId = booking.RoomId;
            _context.SaveChanges();

            var hotel = _context.Hotels.First(h => h.HotelId == room.HotelId);
            var city = _context.Cities.First(c => c.CityId == hotel.CityId);
            return new BookingResponse
            {
                GuestName = user.Name,
                BookingId = bookingToUpdate.BookingId,
                Checkin = bookingToUpdate.CheckIn,
                Checkout = bookingToUpdate.CheckOut,
                GuestQuant = bookingToUpdate.GuestQuant,
                Room = new RoomDto
                {
                    RoomId = room.RoomId,
                    Name = room.Name,
                    Capacity = room.Capacity,
                    Image = room.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = hotel.HotelId,
                        Name = hotel.Name,
                        Address = hotel.Address,
                        CityId = city.CityId,
                        CityName = city.Name,
                        State = city.State
                    }
                }
            };

    }

        public void DeleteBooking(int bookingId)
        {
            var booking = _context.Bookings.First(b => b.BookingId == bookingId);
            if (booking == null)
            {
                throw new InvalidOperationException("Booking not found");
            }
            _context.Bookings.Remove(booking);
            _context.SaveChanges();
        }
    }
}