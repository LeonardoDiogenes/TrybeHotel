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
    public TrybeHotelContext(DbContextOptions<TrybeHotelContext> options) : base(options)
    {
    }
    public TrybeHotelContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Database=TrybeHotel;User=SA;Password=TrybeHotel12!;TrustServerCertificate=True";
        optionsBuilder.UseSqlServer(connectionString);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração da conversão da propriedade Image
        modelBuilder.Entity<Hotel>()
            .Property(h => h.Image)
            .HasConversion(
                v => string.Join(";", v),  // Converter List<string> para string ao salvar no banco de dados
                v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()  // Converter string para List<string> ao ler do banco de dados
            );

        modelBuilder.Entity<Room>()
            .Property(r => r.Image)
            .HasConversion(
                v => string.Join(";", v),  // Converter string para string ao salvar no banco de dados
                v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()  // Converter string para Uri ao ler do banco de dados
            );

        modelBuilder.Entity<City>().HasData(
            new City { CityId = 1, Name = "São Paulo", State = "SP" },
            new City { CityId = 2, Name = "Rio de Janeiro", State = "RJ" }
        );

        // Atualização dos seeders de hotéis
        modelBuilder.Entity<Hotel>().HasData(
            new Hotel
            {
                HotelId = 1,
                Name = "San Raphael Hotel",
                Address = "Largo do Arouche, 150",
                CityId = 1,
                Image = new List<string> {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/17623873.jpg?k=b952cb80dc3ecc4df3b662e9544c8ef0a2cfd480c92d3654d86c9254d7bcff4e&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/44459472.jpg?k=072ac9d3e1405cd3e8a922cddf710f955c912da68ac3433acfed8f73adc9c513&o=&hp=1"
                }
            },
            new Hotel
            {
                HotelId = 2,
                Name = "Hotel Atlântico Business Centro",
                Address = "Rua Senador Dantas, 25",
                CityId = 2,
                Image = new List<string> {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/5139993.jpg?k=19d18100c9cfd4fce562972ace815dbe9e6b91d88d400d10be3510a64ec616e8&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/46891345.jpg?k=b65862e317f0e83af7026b60d73c2b52925a098f1bfdbc2d4a4b5f24a0fff585&o=&hp=1"
                }
            }
        );

        modelBuilder.Entity<Room>().HasData(
            new Room
            {
                RoomId = 1,
                Name = "Quarto 1",
                HotelId = 1,
                Capacity = 2,
                Image = new List<string> {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/360677173.jpg?k=c1083d43ddb2d9415b25304a6f7cd78100e7c737eaacd33273f67b3c904c4a5c&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/266289735.jpg?k=291d57f71acd1295c410553c4d052f0e299c0f2f48bb83595cf7bf2b97238513&o=&hp=1"
                }
            },
            new Room
            {
                RoomId = 2,
                Name = "Quarto 2",
                HotelId = 2,
                Capacity = 3,
                Image = new List<string> {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/46891345.jpg?k=b65862e317f0e83af7026b60d73c2b52925a098f1bfdbc2d4a4b5f24a0fff585&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/46903249.jpg?k=f8fb07eb763acad38a4804b58bc05f3dc285d89338d1d6ac02b31e6f35bd5a1e&o=&hp=1"
                }
            }
        );

        modelBuilder.Entity<User>().HasData(
            new User { UserId = 1, Name = "User 1", Email = "example@example", Password = "123", UserType = "admin" },
            new User { UserId = 2, Name = "User 2", Email = "example2@example", Password = "123", UserType = "client" }
        );

        modelBuilder.Entity<Booking>().HasData(
            new Booking { BookingId = 1, UserId = 1, RoomId = 1, CheckIn = DateTime.Now, CheckOut = DateTime.Now.AddDays(1), GuestQuant = 2 },
            new Booking { BookingId = 2, UserId = 2, RoomId = 2, CheckIn = DateTime.Now, CheckOut = DateTime.Now.AddDays(1), GuestQuant = 1 }
        );
    }


}