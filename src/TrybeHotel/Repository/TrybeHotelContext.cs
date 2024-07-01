using Microsoft.EntityFrameworkCore;
using TrybeHotel.Models;

namespace TrybeHotel.Repository;
public class TrybeHotelContext : DbContext, ITrybeHotelContext
{
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<Hotel> Hotels { get; set; } = null!;
    public DbSet<Room> Rooms { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;
    public TrybeHotelContext(DbContextOptions<TrybeHotelContext> options) : base(options) {
    }
    public TrybeHotelContext() { }
    
       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Database=TrybeHotel;User=SA;Password=TrybeHotel12!;TrustServerCertificate=True";
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<City>().HasData(
            new City {CityId = 1, Name = "São Paulo", State = "SP"},
            new City {CityId = 2, Name = "Rio de Janeiro", State = "RJ"}
        );

        modelBuilder.Entity<Hotel>().HasData(
            new Hotel {HotelId = 1, Name = "Hotel 1", Address = "Endereço 1", CityId = 1},
            new Hotel {HotelId = 2, Name = "Hotel 2", Address = "Endereço 2", CityId = 2}
        );

        modelBuilder.Entity<Room>().HasData(
            new Room {RoomId = 1, Name = "Quarto 1", HotelId = 1, Capacity = 2, Image = "https://via.placeholder.com/150"},
            new Room {RoomId = 2, Name = "Quarto 2", HotelId = 2, Capacity = 3, Image = "https://via.placeholder.com/150"}
        );

        modelBuilder.Entity<User>().HasData(
            new User {UserId = 1, Name = "User 1", Email = "example@example", Password = "123", UserType = "Admin"},
            new User {UserId = 2, Name = "User 2", Email = "example2@example", Password = "123", UserType = "User"}
        );

        modelBuilder.Entity<Booking>().HasData(
            new Booking {BookingId = 1, UserId = 1, RoomId = 1, CheckIn = DateTime.Now, CheckOut = DateTime.Now.AddDays(1), GuestQuant = 2},
            new Booking {BookingId = 2, UserId = 2, RoomId = 2, CheckIn = DateTime.Now, CheckOut = DateTime.Now.AddDays(1), GuestQuant = 1}
        );
    }

}