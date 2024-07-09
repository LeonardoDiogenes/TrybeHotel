// using TrybeHotel.Models;
// using Newtonsoft.Json;
// using TrybeHotel.Test;
// using Microsoft.AspNetCore.Mvc.Testing;
// using TrybeHotel.Dto;
// using System.Text;
// using System.Net.Http.Headers;

// public class CityTests : IntegrationTestBase
// {
// 	public CityTests(WebApplicationFactory<Program> factory) : base(factory) { }

// 	[Trait("City", "GetAll")]
// 	[Theory(DisplayName = "Should return all cities")]
// 	[InlineData("/city")]
// 	public async Task TestGetCity(string url)
//     {
//         var response = await _clientTest.GetAsync(url);
//         var responseData = await response.Content.ReadAsStringAsync();
//         var cities = JsonConvert.DeserializeObject<List<City>>(responseData);

//         Assert.Equal(System.Net.HttpStatusCode.OK, response?.StatusCode);
//         Assert.NotNull(cities);
//         Assert.Contains(cities, c => c.Name == "Manaus" && c.State == "AM");
//         Assert.Contains(cities, c => c.Name == "Palmas" && c.State == "TO");

//     }

// 	[Trait("City", "PostCity")]
// 	[Theory(DisplayName = "Should create a new city")]
// 	[InlineData("/city")]
// 	public async Task TestPostCity(string url)
// 	{
// 		var token = await GetAuthTokenAsync();
//         var city = new City {
//             Name = "Recife",
//             State = "PE"
//         };

//         var json = JsonConvert.SerializeObject(city);
//         var content = new StringContent(json, Encoding.UTF8, "application/json");
// 		_clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

//         var response = await _clientTest.PostAsync(url, content);
//         var responseContent = await response.Content.ReadAsStringAsync();
//         var newCity = JsonConvert.DeserializeObject<CityDto>(responseContent);

//         Assert.Equal(System.Net.HttpStatusCode.Created, response?.StatusCode);
//         Assert.NotNull(newCity);
//         Assert.Equal(city.Name, newCity!.Name);
//         Assert.Equal(city.State, newCity!.State);
// 	}

// 	[Trait("City", "PostCity")]
// 	[Theory(DisplayName = "Should return error when creating a city with invalid data")]
// 	[InlineData("/city")]
// 	public async Task TestPostCityInvalidData(string url)
// 	{
// 		var token = await GetAuthTokenAsync();
// 		var city = new City {
// 			Name = "Brasília",
// 		};

// 		var json = JsonConvert.SerializeObject(city);
// 		var content = new StringContent(json, Encoding.UTF8, "application/json");
// 		_clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

// 		var response = await _clientTest.PostAsync(url, content);
// 		var responseContent = await response.Content.ReadAsStringAsync();

// 		Assert.Equal(System.Net.HttpStatusCode.BadRequest, response?.StatusCode);
// 		Assert.Contains("Invalid data", responseContent);
// 	}

// 	[Trait("City", "PutCity")]
// 	[Theory(DisplayName = "Should update a city")]
// 	[InlineData("/city")]
// 	public async Task TestPutCity(string url)
// 	{
// 		var token = await GetAuthTokenAsync();
// 		var city = new City {
// 			CityId = 1,
// 			Name = "Manaus Teste",
// 			State = "AM"
// 		};

// 		var json = JsonConvert.SerializeObject(city);
// 		var content = new StringContent(json, Encoding.UTF8, "application/json");
// 		_clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

// 		var response = await _clientTest.PutAsync(url, content);
// 		var responseContent = await response.Content.ReadAsStringAsync();
// 		var updatedCity = JsonConvert.DeserializeObject<CityDto>(responseContent);

// 		Assert.Equal(System.Net.HttpStatusCode.OK, response?.StatusCode);
// 		Assert.NotNull(updatedCity);
// 		Assert.Equal(city.Name, updatedCity!.Name);
// 		Assert.Equal(city.State, updatedCity!.State);
// 	}

// 	[Trait("City", "PutCity")]
// 	[Theory(DisplayName = "Should return error when updating a city that does not exist")]
// 	[InlineData("/city")]
// 	public async Task TestPutCityError(string url)
// 	{
// 		var token = await GetAuthTokenAsync();
// 		var city = new City {
// 			CityId = 999,
// 			Name = "Manaus",
// 			State = "AM"
// 		};

// 		var json = JsonConvert.SerializeObject(city);
// 		var content = new StringContent(json, Encoding.UTF8, "application/json");
// 		_clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

// 		var response = await _clientTest.PutAsync(url, content);
// 		var responseContent = await response.Content.ReadAsStringAsync();

// 		Assert.Equal(System.Net.HttpStatusCode.BadRequest, response?.StatusCode);
// 		Assert.Contains("City not found", responseContent);
// 	}

// 	[Trait("City", "DeleteCity")]
// 	[Theory(DisplayName = "Should delete a city")]
// 	[InlineData("/city/1")]
// 	public async Task TestDeleteCity(string url)
// 	{
// 		var token = await GetAuthTokenAsync();
// 		_clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

// 		var response = await _clientTest.DeleteAsync(url);

// 		Assert.Equal(System.Net.HttpStatusCode.NoContent, response?.StatusCode);
// 	}

// 	[Trait("City", "DeleteCity")]
// 	[Theory(DisplayName = "Should return error when deleting a city that does not exist")]
// 	[InlineData("/city/999")]
// 	public async Task TestDeleteCityError(string url)
// 	{
// 		var token = await GetAuthTokenAsync();
// 		_clientTest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

// 		var response = await _clientTest.DeleteAsync(url);
// 		var responseContent = await response.Content.ReadAsStringAsync();

// 		Assert.Equal(System.Net.HttpStatusCode.BadRequest, response?.StatusCode);
// 		Assert.Contains("City not found", responseContent);
// 	}
// }