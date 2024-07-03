namespace TrybeHotel.Test;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Diagnostics;
using System.Xml;
using System.IO;
using TrybeHotel.Dto;
using System.Text;

public class LoginJson {
    public string? token { get; set; }
}


public class IntegrationTest: IClassFixture<WebApplicationFactory<Program>>
{
     public HttpClient _clientTest;

     public IntegrationTest(WebApplicationFactory<Program> factory)
    {
        //_factory = factory;
        _clientTest = factory.WithWebHostBuilder(builder => {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TrybeHotelContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ContextTest>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryTestDatabase");
                });
                services.AddScoped<ITrybeHotelContext, ContextTest>();
                services.AddScoped<ICityRepository, CityRepository>();
                services.AddScoped<IHotelRepository, HotelRepository>();
                services.AddScoped<IRoomRepository, RoomRepository>();
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<ContextTest>())
                {
                    appContext.Database.EnsureCreated();
                    appContext.Database.EnsureDeleted();
                    appContext.Database.EnsureCreated();
                    appContext.Cities.Add(new City {CityId = 1, Name = "Manaus", State = "AM"});
                    appContext.Cities.Add(new City {CityId = 2, Name = "Palmas", State = "TO"});
                    appContext.SaveChanges();
                    appContext.Hotels.Add(new Hotel {HotelId = 1, Name = "Trybe Hotel Manaus", Address = "Address 1", CityId = 1});
                    appContext.Hotels.Add(new Hotel {HotelId = 2, Name = "Trybe Hotel Palmas", Address = "Address 2", CityId = 2});
                    appContext.Hotels.Add(new Hotel {HotelId = 3, Name = "Trybe Hotel Ponta Negra", Address = "Addres 3", CityId = 1});
                    appContext.SaveChanges();
                    appContext.Rooms.Add(new Room { RoomId = 1, Name = "Room 1", Capacity = 2, Image = "Image 1", HotelId = 1 });
                    appContext.Rooms.Add(new Room { RoomId = 2, Name = "Room 2", Capacity = 3, Image = "Image 2", HotelId = 1 });
                    appContext.Rooms.Add(new Room { RoomId = 3, Name = "Room 3", Capacity = 4, Image = "Image 3", HotelId = 1 });
                    appContext.Rooms.Add(new Room { RoomId = 4, Name = "Room 4", Capacity = 2, Image = "Image 4", HotelId = 2 });
                    appContext.Rooms.Add(new Room { RoomId = 5, Name = "Room 5", Capacity = 3, Image = "Image 5", HotelId = 2 });
                    appContext.Rooms.Add(new Room { RoomId = 6, Name = "Room 6", Capacity = 4, Image = "Image 6", HotelId = 2 });
                    appContext.Rooms.Add(new Room { RoomId = 7, Name = "Room 7", Capacity = 2, Image = "Image 7", HotelId = 3 });
                    appContext.Rooms.Add(new Room { RoomId = 8, Name = "Room 8", Capacity = 3, Image = "Image 8", HotelId = 3 });
                    appContext.Rooms.Add(new Room { RoomId = 9, Name = "Room 9", Capacity = 4, Image = "Image 9", HotelId = 3 });
                    appContext.SaveChanges();
                    appContext.Users.Add(new User { UserId = 1, Name = "Ana", Email = "ana@trybehotel.com", Password = "Senha1", UserType = "admin" });
                    appContext.Users.Add(new User { UserId = 2, Name = "Beatriz", Email = "beatriz@trybehotel.com", Password = "Senha2", UserType = "client" });
                    appContext.Users.Add(new User { UserId = 3, Name = "Laura", Email = "laura@trybehotel.com", Password = "Senha3", UserType = "client" });
                    appContext.SaveChanges();
                    appContext.Bookings.Add(new Booking { BookingId = 1, CheckIn = new DateTime(2023, 07, 02), CheckOut = new DateTime(2023, 07, 03), GuestQuant = 1, UserId = 2, RoomId = 1});
                    appContext.Bookings.Add(new Booking { BookingId = 2, CheckIn = new DateTime(2023, 07, 02), CheckOut = new DateTime(2023, 07, 03), GuestQuant = 1, UserId = 3, RoomId = 4});
                    appContext.SaveChanges();
                }
            });
        }).CreateClient();
    }
 
    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Executando meus testes")]
    [InlineData("/city")]
    public async Task TestGet(string url)
    {
        var response = await _clientTest.GetAsync(url);
        var responseData = await response.Content.ReadAsStringAsync();
        var cities = JsonConvert.DeserializeObject<List<City>>(responseData);

        Assert.Equal(System.Net.HttpStatusCode.OK, response?.StatusCode);
        Assert.NotNull(cities);
        Assert.Contains(cities, c => c.Name == "Manaus" && c.State == "AM");
        Assert.Contains(cities, c => c.Name == "Palmas" && c.State == "TO");

    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Executando meus testes")]
    [InlineData("/hotel")]
    public async Task TestGetHotel(string url)
    {
        var response = await _clientTest.GetAsync(url);
        var responseData = await response.Content.ReadAsStringAsync();
        var hotels = JsonConvert.DeserializeObject<List<Hotel>>(responseData);

        Assert.Equal(System.Net.HttpStatusCode.OK, response?.StatusCode);
        Assert.NotNull(hotels);
        Assert.Contains(hotels, c => c.Name == "Trybe Hotel Manaus" && c.Address == "Address 1");
        Assert.Contains(hotels, c => c.Name == "Trybe Hotel Palmas" && c.Address == "Address 2");
        Assert.Contains(hotels, c => c.Name == "Trybe Hotel Ponta Negra" && c.Address == "Addres 3");
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Executando meus testes")]
    [InlineData("/room/1")] //HotelId
    public async Task TestGetRoom(string url)
    {
        var response = await _clientTest.GetAsync(url);
        var responseData = await response.Content.ReadAsStringAsync();
        var rooms = JsonConvert.DeserializeObject<List<Room>>(responseData);

        Assert.Equal(System.Net.HttpStatusCode.OK, response?.StatusCode);
        Assert.NotNull(rooms);
        Assert.Contains(rooms, c => c.Name == "Room 1" && c.Capacity == 2);
        Assert.Contains(rooms, c => c.Name == "Room 2" && c.Capacity == 3);
        Assert.Contains(rooms, c => c.Name == "Room 3" && c.Capacity == 4);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Executando meus testes")]
    [InlineData("/user")]
    public async Task TestPostUser(string url)
    {
        var user = new UserDtoInsert {
            Name = "Carlos",
            Email = "carlos@exemplo.com",
            Password = "Senha123"
        };
        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _clientTest.PostAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();
        var newUser = JsonConvert.DeserializeObject<UserDto>(responseContent);

        Assert.Equal(System.Net.HttpStatusCode.Created, response?.StatusCode);
        Assert.NotNull(newUser);
        Assert.Equal(user.Name, newUser!.Name);
        Assert.Equal(user.Email, newUser!.Email);
        Assert.Equal("client", newUser!.UserType);
        
    }

    
}