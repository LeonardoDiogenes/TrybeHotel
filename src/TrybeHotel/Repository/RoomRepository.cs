using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
            var rooms = from room in _context.Rooms
                        where room.HotelId == HotelId
                        select new RoomDto
                        {
                            RoomId = room.RoomId,
                            Name = room.Name,
                            Capacity = room.Capacity,
                            Image = room.Image,
                            Hotel = new HotelDto
                            {
                                HotelId = room.Hotel!.HotelId,
                                Name = room.Hotel.Name,
                                Address = room.Hotel.Address,
                                CityId = room.Hotel.CityId,
                                CityName = room.Hotel.City!.Name,
                                State = room.Hotel.City!.State
                            }
                        };
            return rooms.ToList();
        }

        public RoomDto AddRoom(Room room) {
            try
            {
                _context.Rooms.Add(room);
                _context.SaveChanges();
                var newRoom = _context.Rooms.First(r => r.RoomId == room.RoomId);
                newRoom.Hotel = _context.Hotels.First(h => h.HotelId == room.HotelId);
                newRoom.Hotel.City = _context.Cities.First(c => c.CityId == newRoom.Hotel.CityId);
                return new RoomDto
                {
                    RoomId = newRoom.RoomId,
                    Name = newRoom.Name,
                    Capacity = newRoom.Capacity,
                    Image = newRoom.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = newRoom.Hotel.HotelId,
                        Name = newRoom.Hotel.Name,
                        Address = newRoom.Hotel.Address,
                        CityId = newRoom.Hotel.CityId,
                        CityName = newRoom.Hotel.City.Name,
                        State = newRoom.Hotel.City.State
                    }
                };
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public void DeleteRoom(int RoomId) {
            var room = _context.Rooms.Find(RoomId);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }
        }
    }
}