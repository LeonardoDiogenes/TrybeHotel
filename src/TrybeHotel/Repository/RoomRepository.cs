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
            var hotel = _context.Hotels.Find(HotelId);
            if (hotel == null)
            {
                throw new Exception("Hotel not found");
            }
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
            if (rooms.Count() == 0)
            {
                throw new Exception("No rooms found");
            }
            return rooms;
        }

        public RoomDto AddRoom(Room room) {
            var roomHotel = _context.Hotels.Find(room.HotelId);
            if (roomHotel == null)
            {
                throw new Exception("Hotel not found");
            }
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

        public void DeleteRoom(int RoomId) {
            var room = _context.Rooms.Find(RoomId);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Room not found");
            }
        }

        public RoomDto UpdateRoom(int RoomId, Room room) {
            var roomToUpdate = _context.Rooms.Find(RoomId);
            if (roomToUpdate == null)
            {
                throw new Exception("Room not found");
            }

            roomToUpdate.Name = room.Name;
            roomToUpdate.Capacity = room.Capacity;
            roomToUpdate.Image = room.Image;
            _context.SaveChanges();

            roomToUpdate.Hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == room.HotelId);
            roomToUpdate.Hotel!.City = _context.Cities.FirstOrDefault(c => c.CityId == roomToUpdate.Hotel.CityId);

            return new RoomDto
            {
                RoomId = roomToUpdate.RoomId,
                Name = roomToUpdate.Name,
                Capacity = roomToUpdate.Capacity,
                Image = roomToUpdate.Image,
                Hotel = new HotelDto
                {
                    HotelId = roomToUpdate.Hotel.HotelId,
                    Name = roomToUpdate.Hotel.Name,
                    Address = roomToUpdate.Hotel.Address,
                    CityId = roomToUpdate.Hotel.CityId,
                    CityName = roomToUpdate.Hotel.City!.Name,
                    State = roomToUpdate.Hotel.City.State
                }
            };
        }
    }
}