namespace TrybeHotel.Dto {
     public class RoomDto {
           public int RoomId {get; set;}
           public string? Name {get; set;}
           public int Capacity {get; set;}
           public int KingSizeBeds {get; set;}
           public int SingleSizeBeds {get; set;}
           public List<string>? Image {get; set;}
           public HotelDto? Hotel {get; set;}
     }
}