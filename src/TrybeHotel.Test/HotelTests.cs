using TrybeHotel.Models;
using Newtonsoft.Json;
using TrybeHotel.Test;
using Microsoft.AspNetCore.Mvc.Testing;
using TrybeHotel.Dto;
using System.Text;
using System.Net.Http.Headers;

public class HotelTests : IntegrationTestBase
{
  public HotelTests(WebApplicationFactory<Program> factory) : base(factory) { }

  [Trait("Hotel", "GetAll")]
  [Theory(DisplayName = "Should return all hotels")]
  [InlineData("/hotel")]
  public async Task TestGetHotel(string url)
  {
    var response = await _clientTest.GetAsync(url);
    var responseData = await response.Content.ReadAsStringAsync();
    var hotels = JsonConvert.DeserializeObject<List<Hotel>>(responseData);

    Assert.Equal(System.Net.HttpStatusCode.OK, response?.StatusCode);
    Assert.NotNull(hotels);
    Assert.Contains(hotels, h => h.Name == "Trybe Hotel Manaus" && h.CityId == 1);
    Assert.Contains(hotels, h => h.Name == "Trybe Hotel Palmas" && h.CityId == 2);
    Assert.Contains(hotels, h => h.Name == "Trybe Hotel Ponta Negra" && h.CityId == 1);
  }
}