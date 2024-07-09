// namespace TrybeHotel.Test;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Mvc.Testing;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using Newtonsoft.Json;
// using System.Net.Http.Headers;
// using System.Text;
// using System.Threading.Tasks;
// using TrybeHotel.Models;
// using TrybeHotel.Repository;
// using TrybeHotel.Dto;



//   public class LoginJson {
//       public string token { get; set; } = null!;
//   }
  
//   public class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>
//   {
//       protected readonly HttpClient _clientTest;
//       private readonly WebApplicationFactory<Program>? _factory;

//       public IntegrationTestBase(WebApplicationFactory<Program> factory)
//       {
//           _factory = factory;
//           _clientTest = _factory.WithWebHostBuilder(builder =>
//           {
//               builder.ConfigureServices(services =>
//               {
//                   var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TrybeHotelContext>));
//                   if (descriptor != null)
//                   {
//                       services.Remove(descriptor);
//                   }

//                   services.AddDbContext<ContextTest>(options =>
//                   {
//                       options.UseInMemoryDatabase("InMemoryTestDatabase");
//                   });
//                   services.AddScoped<ITrybeHotelContext, ContextTest>();
//                   services.AddScoped<ICityRepository, CityRepository>();
//                   services.AddScoped<IHotelRepository, HotelRepository>();
//                   services.AddScoped<IRoomRepository, RoomRepository>();

//                   var sp = services.BuildServiceProvider();
//                   using (var scope = sp.CreateScope())
//                   using (var appContext = scope.ServiceProvider.GetRequiredService<ContextTest>())
//                   {
//                       appContext.Database.EnsureDeleted();
//                       appContext.Database.EnsureCreated();
//                       InitializeData(appContext);
//                   }
//               });
//           }).CreateClient();
//       }

//       private void InitializeData(ContextTest context)
//       {

//           context.Cities.RemoveRange(context.Cities);
//           context.Hotels.RemoveRange(context.Hotels);
//           context.Rooms.RemoveRange(context.Rooms);
//           context.Users.RemoveRange(context.Users);
//           context.Bookings.RemoveRange(context.Bookings);
//           context.SaveChanges();

//           context.Cities.AddRange(
//               new City { CityId = 1, Name = "Manaus", State = "AM" },
//               new City { CityId = 2, Name = "Palmas", State = "TO" }
//           );
//           context.SaveChanges();

//           context.Hotels.AddRange(
//               new Hotel { HotelId = 1, Name = "Trybe Hotel Manaus", Address = "Address 1", CityId = 1 },
//               new Hotel { HotelId = 2, Name = "Trybe Hotel Palmas", Address = "Address 2", CityId = 2 },
//               new Hotel { HotelId = 3, Name = "Trybe Hotel Ponta Negra", Address = "Addres 3", CityId = 1 }
//           );
//           context.SaveChanges();

//           context.Rooms.AddRange(
//               new Room { RoomId = 1, Name = "Room 1", Capacity = 2, Image = "Image 1", HotelId = 1 },
//               new Room { RoomId = 2, Name = "Room 2", Capacity = 3, Image = "Image 2", HotelId = 1 },
//               new Room { RoomId = 3, Name = "Room 3", Capacity = 4, Image = "Image 3", HotelId = 1 },
//               new Room { RoomId = 4, Name = "Room 4", Capacity = 2, Image = "Image 4", HotelId = 2 },
//               new Room { RoomId = 5, Name = "Room 5", Capacity = 3, Image = "Image 5", HotelId = 2 },
//               new Room { RoomId = 6, Name = "Room 6", Capacity = 4, Image = "Image 6", HotelId = 2 },
//               new Room { RoomId = 7, Name = "Room 7", Capacity = 2, Image = "Image 7", HotelId = 3 },
//               new Room { RoomId = 8, Name = "Room 8", Capacity = 3, Image = "Image 8", HotelId = 3 },
//               new Room { RoomId = 9, Name = "Room 9", Capacity = 4, Image = "Image 9", HotelId = 3 }
//           );
//           context.SaveChanges();

//           context.Users.AddRange(
//               new User { UserId = 1, Name = "Ana", Email = "ana@trybehotel.com", Password = "Senha1", UserType = "admin" },
//               new User { UserId = 2, Name = "Beatriz", Email = "beatriz@trybehotel.com", Password = "Senha2", UserType = "client" },
//               new User { UserId = 3, Name = "Laura", Email = "laura@trybehotel.com", Password = "Senha3", UserType = "client" }
//           );
//           context.SaveChanges();

//           context.Bookings.AddRange(
//               new Booking { BookingId = 1, CheckIn = new DateTime(2023, 07, 02), CheckOut = new DateTime(2023, 07, 03), GuestQuant = 1, UserId = 2, RoomId = 1 },
//               new Booking { BookingId = 2, CheckIn = new DateTime(2023, 07, 02), CheckOut = new DateTime(2023, 07, 03), GuestQuant = 1, UserId = 3, RoomId = 4 }
//           );
//           context.SaveChanges();
//       }

//       protected async Task<string?> GetAuthTokenAsync()
//       {
//           var loginBody = new LoginDto
//           {
//               Email = "ana@trybehotel.com",
//               Password = "Senha1"
//           };
//           var jsonBody = JsonConvert.SerializeObject(loginBody);
//           var loginContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
//           var loginResponse = await _clientTest.PostAsync("/login", loginContent);
//           var loginResponseContent = await loginResponse.Content.ReadAsStringAsync();
//           var tokenObject = JsonConvert.DeserializeObject<LoginJson>(loginResponseContent);
//           return tokenObject?.token;
//       }
//   }

