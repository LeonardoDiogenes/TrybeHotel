using TrybeHotel.Dto;
using TrybeHotel.Repository;

namespace TrybeHotel.Services
{
    public interface IGeoService
    {
        Task<object> GetGeoStatus();
        Task<List<GeoDtoHotelResponse>> GetHotelsByGeo(GeoDto geoDto, IHotelRepository repository);

        Task<GeoDtoResponse> GetGeoLocation(GeoDto geoDto);

        Task<List<RoomDtoResponse>> GetRoomsByGeo(GeoDto geoDto, IRoomRepository repository);
    }
}