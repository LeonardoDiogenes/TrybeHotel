namespace TrybeHotel.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// 1. Implemente as models da aplicação
public class User {
	[Key]
	public int UserId { get; set; }
	[Required]
	public string? Name { get; set; }
	[Required]
	public string? Email { get; set; }
	[Required]
	public string? Password { get; set; }
	[Required]
	public string? UserType { get; set; }
	public List<Booking>? Bookings { get; set; }
}