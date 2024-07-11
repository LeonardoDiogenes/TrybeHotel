namespace TrybeHotel.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// 1. Implemente as models da aplicação
public class Room {
	public int RoomId {get; set;}
	[Required(ErrorMessage = "Name is required")]
	[StringLength(15, ErrorMessage = "Name must have a maximum of 15 characters")]
	public string? Name {get; set;}
	[Required(ErrorMessage = "Capacity is required")]
	[Range(1, 10, ErrorMessage = "Capacity must be between 1 and 10")]
	public int Capacity {get; set;}
	public string? Image {get; set;}
	[Required(ErrorMessage = "HotelId is required")]
	public int HotelId {get; set;}
	public Hotel? Hotel {get; set;}
	public ICollection<Booking>? Bookings { get; set; }
}