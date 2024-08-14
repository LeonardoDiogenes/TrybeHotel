namespace TrybeHotel.Dto
{
   public class GeoDto
   {
      public string? Address { get; set; }
      public string? City { get; set; }
      public string? State { get; set; }
   }

   public class GeoDtoResponse
   {
      public string? lat { get; set; }
      public string? lon { get; set; }
   }

   public class GeoDtoHotelResponse
   {
      public int HotelId { get; set; }
      public string? Name { get; set; }
      public string? Address { get; set; }
      public List<string>? Image { get; set; }
      public string? CityName { get; set; }
      public string? State { get; set; }
      public int Distance { get; set; }
   }

   public class RoomDtoResponse
   {
      public int RoomId { get; set; }
      public string? Name { get; set; }
      public int Capacity { get; set; }
      public string? Image { get; set; }
      public HotelDto? Hotel { get; set; }
      public int Distance { get; set; }
   }
}