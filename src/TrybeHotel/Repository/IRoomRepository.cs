using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public interface IRoomRepository
    {
        IEnumerable<RoomDto> GetAllRooms();
        
        IEnumerable<RoomDto> GetRooms(int HotelId);
        RoomDto AddRoom(Room room);

        void DeleteRoom(int RoomId);

        RoomDto UpdateRoom(int RoomId, Room room);
    }
}