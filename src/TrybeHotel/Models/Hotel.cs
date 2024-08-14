namespace TrybeHotel.Models;

public class Hotel {
	public int HotelId {get; set;}
	public List<string>? Image {get; set;}
	public string? Name {get; set;}
	public string? Address {get; set;}

	public int CityId {get; set;}
	public City? City {get; set;}

	public virtual List<Room>? Rooms {get; set;}
}

