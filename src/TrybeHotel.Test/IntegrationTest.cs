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
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;


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

    private async Task<string?> GetAuthTokenAsync()
    {
        var loginBody = new LoginDto {
            Email = "ana@trybehotel.com",
            Password = "Senha1"
        };
        var jsonBody = JsonConvert.SerializeObject(loginBody);
        var loginContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        var loginResponse = await _clientTest.PostAsync("/login", loginContent);
        var loginResponseContent = await loginResponse.Content.ReadAsStringAsync();
        var tokenObject = JsonConvert.DeserializeObject<LoginJson>(loginResponseContent);
        return tokenObject?.token;
    }
 
    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing GET /city")]
    [InlineData("/city")]
    public async Task TestGetCity(string url)
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
    [Theory(DisplayName = "Testing GET /hotel")]
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
    [Theory(DisplayName = "Testing GET /room/{hotelId}")]
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
    [Theory(DisplayName = "Testing POST /user")]
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

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing POST /login")]
    [InlineData("/login")]
    public async Task TestPostLogin(string url)
    {
        var loginBody = new LoginDto {
            Email = "ana@trybehotel.com",
            Password = "Senha1"
        };
        var jsonBody = JsonConvert.SerializeObject(loginBody);
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        var response = await _clientTest.PostAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.OK, response?.StatusCode);
        var token = JsonConvert.DeserializeObject<LoginJson>(responseContent);
        Assert.NotNull(token);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing POST /city")]
    [InlineData("/city")]
    public async Task TestPostCity(string url)
    {
        var token = await GetAuthTokenAsync();

        var city = new City {
            Name = "Recife",
            State = "PE"
        };
        var json = JsonConvert.SerializeObject(city);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.PostAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();
        var newCity = JsonConvert.DeserializeObject<CityDto>(responseContent);

        Assert.Equal(System.Net.HttpStatusCode.Created, response?.StatusCode);
        Assert.NotNull(newCity);
        Assert.Equal(city.Name, newCity!.Name);
        Assert.Equal(city.State, newCity!.State);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing PUT /city")]
    [InlineData("/city")]
    public async Task TestPutCity(string url)
    {
        var token = await GetAuthTokenAsync();

        var city = new City {
            CityId = 1,
            Name = "Recife",
            State = "PE"
        };
        var json = JsonConvert.SerializeObject(city);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.PutAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();
        var newCity = JsonConvert.DeserializeObject<CityDto>(responseContent);

        Assert.Equal(System.Net.HttpStatusCode.OK, response?.StatusCode);
        Assert.NotNull(newCity);
        Assert.Equal(city.Name, newCity!.Name);
        Assert.Equal(city.State, newCity!.State);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing DELETE /city")]
    [InlineData("/city/1")]
    public async Task TestDeleteCity(string url)
    {
        var token = await GetAuthTokenAsync();

        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.DeleteAsync(url);

        Assert.Equal(System.Net.HttpStatusCode.NoContent, response?.StatusCode);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing PUT /city error")]
    [InlineData("/city")]
    public async Task TestPutCityError(string url)
    {
        var token = await GetAuthTokenAsync();

        var city = new City {
            CityId = 999,
            Name = "Recife",
            State = "PE"
        };
        var json = JsonConvert.SerializeObject(city);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.PutAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response?.StatusCode);
        Assert.Equal("City not found", responseContent);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing DELETE /city error")]
    [InlineData("/city/999")]
    public async Task TestDeleteCityError(string url)
    {
        var token = await GetAuthTokenAsync();

        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.DeleteAsync(url);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response?.StatusCode);
        Assert.Equal("City not found", responseContent);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing POST /hotel")]
    [InlineData("/hotel")]
    public async Task TestPostHotel(string url)
    {
        var token = await GetAuthTokenAsync();

        var hotel = new Hotel {
            Name = "Hotel Teste",
            Address = "Address Teste",
            CityId = 1
        };
        var json = JsonConvert.SerializeObject(hotel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.PostAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();
        var newHotel = JsonConvert.DeserializeObject<HotelDto>(responseContent);

        Assert.Equal(System.Net.HttpStatusCode.Created, response?.StatusCode);
        Assert.NotNull(newHotel);
        Assert.Equal(hotel.Name, newHotel!.Name);
        Assert.Equal(hotel.Address, newHotel!.Address);
        Assert.Equal(hotel.CityId, newHotel!.CityId);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing POST /hotel error")]
    [InlineData("/hotel")]
    public async Task TestPostHotelError(string url)
    {
        var token = await GetAuthTokenAsync();

        var hotel = new Hotel {
            Name = "Hotel Teste",
            Address = "Address Teste",
            CityId = 999
        };
        var json = JsonConvert.SerializeObject(hotel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.PostAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response?.StatusCode);
        Assert.Equal("Error adding hotel", responseContent);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing PUT /hotel")]
    [InlineData("/hotel")]
    public async Task TestPutHotel(string url)
    {
        var token = await GetAuthTokenAsync();

        var hotel = new Hotel {
            HotelId = 1,
            Name = "Hotel Teste",
            Address = "Address Teste",
            CityId = 1
        };
        var json = JsonConvert.SerializeObject(hotel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.PutAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();
        var newHotel = JsonConvert.DeserializeObject<HotelDto>(responseContent);

        Assert.Equal(System.Net.HttpStatusCode.OK, response?.StatusCode);
        Assert.NotNull(newHotel);
        Assert.Equal(hotel.Name, newHotel!.Name);
        Assert.Equal(hotel.Address, newHotel!.Address);
        Assert.Equal(hotel.CityId, newHotel!.CityId);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing PUT /hotel error")]
    [InlineData("/hotel")]
    public async Task TestPutHotelError(string url)
    {
        var token = await GetAuthTokenAsync();

        var hotel = new Hotel {
            HotelId = 999,
            Name = "Hotel Teste",
            Address = "Address Teste",
            CityId = 1
        };
        var json = JsonConvert.SerializeObject(hotel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.PutAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response?.StatusCode);
        Assert.Equal("Hotel not found", responseContent);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing DELETE /hotel")]
    [InlineData("/hotel/1")]
    public async Task TestDeleteHotel(string url)
    {
        var token = await GetAuthTokenAsync();

        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.DeleteAsync(url);

        Assert.Equal(System.Net.HttpStatusCode.NoContent, response?.StatusCode);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing DELETE /hotel error")]
    [InlineData("/hotel/999")]
    public async Task TestDeleteHotelError(string url)
    {
        var token = await GetAuthTokenAsync();

        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.DeleteAsync(url);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response?.StatusCode);
        Assert.Equal("Hotel not found", responseContent);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing POST /city error")]
    [InlineData("/city")]
    public async Task TestPostCityError(string url)
    {
        var token = await GetAuthTokenAsync();

        var city = new City {
            Name = "Brasília"
        };

        var json = JsonConvert.SerializeObject(city);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _clientTest.PostAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response?.StatusCode);
        Assert.Equal("Invalid data", responseContent);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing POST /login auth error")]
    [InlineData("/login")]
    public async Task TestLoginError(string url)
    {
        var loginBody = new LoginDto {
            Email = "erroteste@trybehotel.com",
            Password = "Senha1"
        };
        var jsonBody = JsonConvert.SerializeObject(loginBody);
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        var response = await _clientTest.PostAsync(url, content);

        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response?.StatusCode);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing POST /room")]
    [InlineData("/room")]
    public async Task TestPostRoom(string url)
    {
        var token = await GetAuthTokenAsync();

        var room = new Room {
            Name = "Room Teste",
            Capacity = 2,
            Image = "Image Teste",
            HotelId = 1
        };
        var json = JsonConvert.SerializeObject(room);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.PostAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();
        var newRoom = JsonConvert.DeserializeObject<RoomDto>(responseContent);

        Assert.Equal(System.Net.HttpStatusCode.Created, response?.StatusCode);
        Assert.NotNull(newRoom);
        Assert.Equal(room.Name, newRoom!.Name);
        Assert.Equal(room.Capacity, newRoom!.Capacity);
        Assert.Equal(room.Image, newRoom!.Image);
        Assert.Equal(room.HotelId, newRoom!.Hotel!.HotelId);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing DELETE /room")]
    [InlineData("/room/1")]
    public async Task TestDeleteRoom(string url)
    {
        var token = await GetAuthTokenAsync();

        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.DeleteAsync(url);

        Assert.Equal(System.Net.HttpStatusCode.NoContent, response?.StatusCode);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing PUT /room")]
    [InlineData("/room/1")]
    public async Task TestPutRoom(string url)
    {
        var token = await GetAuthTokenAsync();

        var room = new Room {
            Name = "Room Teste",
            Capacity = 2,
            HotelId = 1
        };
        var json = JsonConvert.SerializeObject(room);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.PutAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();
        var newRoom = JsonConvert.DeserializeObject<RoomDto>(responseContent);

        Assert.Equal(System.Net.HttpStatusCode.OK, response?.StatusCode);
        Assert.NotNull(newRoom);
        Assert.Equal(room.Name, newRoom!.Name);
        Assert.Equal(room.Capacity, newRoom!.Capacity);
        Assert.Equal(room.Image, newRoom!.Image);
        Assert.Equal(room.HotelId, newRoom!.Hotel!.HotelId);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing POST /room invalid data error")]
    [InlineData("/room")]
    public async Task TestPostRoomError(string url)
    {
        var token = await GetAuthTokenAsync();

        var room = new Room {
            Capacity = 2,
            Image = "Image Teste",
            HotelId = 1
        };
        var json = JsonConvert.SerializeObject(room);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.PostAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response?.StatusCode);
        Assert.Equal("Invalid data", responseContent);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing POST /room Hotel not found error")]
    [InlineData("/room")]
    public async Task TestPostRoomHotelError(string url)
    {
        var token = await GetAuthTokenAsync();

        var room = new Room {
            Name = "Room Teste",
            Capacity = 2,
            Image = "Image Teste",
            HotelId = 999
        };
        var json = JsonConvert.SerializeObject(room);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.PostAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response?.StatusCode);
        Assert.Equal("Hotel not found", responseContent);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing DELETE /room error")]
    [InlineData("/room/999")]
    public async Task TestDeleteRoomError(string url)
    {
        var token = await GetAuthTokenAsync();

        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.DeleteAsync(url);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response?.StatusCode);
        Assert.Equal("Room not found", responseContent);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing PUT /room error")]
    [InlineData("/room/999")]
    public async Task TestPutRoomError(string url)
    {
        var token = await GetAuthTokenAsync();

        var room = new Room {
            Name = "Room Teste",
            Capacity = 2,
            HotelId = 1
        };
        var json = JsonConvert.SerializeObject(room);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.PutAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response?.StatusCode);
        Assert.Equal("Room not found", responseContent);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing GET /user")]
    [InlineData("/user")]
    public async Task TestGetUser(string url)
    {
        var token = await GetAuthTokenAsync();

        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.GetAsync(url);
        var responseData = await response.Content.ReadAsStringAsync();
        var users = JsonConvert.DeserializeObject<List<UserDto>>(responseData);

        Assert.Equal(System.Net.HttpStatusCode.OK, response?.StatusCode);
        Assert.NotNull(users);
        Assert.Contains(users, c => c.Name == "Ana" && c.Email == "ana@trybehotel.com");
        Assert.Contains(users, c => c.Name == "Beatriz" && c.Email == "beatriz@trybehotel.com");
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing POST /user email already exists ERROR")]
    [InlineData("/user")]
    public async Task TestPostUserError(string url)
    {
        var newUser = new UserDtoInsert {
            Name = "Ana",
            Email = "ana@trybehotel.com",
            Password = "Senha1"
        };
        var json = JsonConvert.SerializeObject(newUser);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _clientTest.PostAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.Conflict, response?.StatusCode);
        var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
        Assert.Equal("User email already exists", (string)responseObject!.message);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing DELETE /user")]
    [InlineData("/user/2")]
    public async Task TestDeleteUser(string url)
    {
        var token = await GetAuthTokenAsync();

        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.DeleteAsync(url);

        Assert.Equal(System.Net.HttpStatusCode.OK, response?.StatusCode);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing DELETE /user error")]
    [InlineData("/user/999")]
    public async Task TestDeleteUserError(string url)
    {
        var token = await GetAuthTokenAsync();

        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.DeleteAsync(url);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.Conflict, response?.StatusCode);
        var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
        Assert.Equal("User not found", (string)responseObject!.message);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing PUT /user")]
    [InlineData("/user/1")]
    public async Task TestPutUser(string url)
    {
        var token = await GetAuthTokenAsync();

        var user = new User {
            Name = "Ana",
            Email = "newAnaEmail@teste.com",
            Password = "Senha1",
            UserType = "admin"
        };

        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.PutAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();
        var newUser = JsonConvert.DeserializeObject<UserDto>(responseContent);

        Assert.Equal(System.Net.HttpStatusCode.OK, response?.StatusCode);
        Assert.NotNull(newUser);
        Assert.Equal(user.Name, newUser!.Name);
        Assert.Equal(user.Email, newUser!.Email);
        Assert.Equal(user.UserType, newUser!.UserType);
    }

    [Trait("Category", "Meus testes")]
    [Theory(DisplayName = "Testing PUT /user error")]
    [InlineData("/user/999")]
    public async Task TestPutUserError(string url)
    {
        var token = await GetAuthTokenAsync();

        var user = new User {
            Name = "Ana",
            Email = "newAnaEmail@teste.com",
            Password = "Senha1",
            UserType = "admin"
        };

        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _clientTest.PutAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.Conflict, response?.StatusCode);
        var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
        Assert.Equal("User not found", (string)responseObject!.message);
    }
    
    
}