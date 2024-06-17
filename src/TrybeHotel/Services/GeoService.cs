using System.Text.Json;
using System.Net.Http;
using TrybeHotel.Dto;
using TrybeHotel.Repository;

namespace TrybeHotel.Services
{
    public class GeoService : IGeoService
    {
         private readonly HttpClient _client;
        public GeoService(HttpClient client)
        {
            _client = client;
        }

        public async Task<object> GetGeoStatus()
        {
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://nominatim.openstreetmap.org/status.php?format=json");
                requestMessage.Headers.Add("Accept", "application/json");
                requestMessage.Headers.Add("User-Agent", "aspnet-user-agent");
                var response = await _client.SendAsync(requestMessage);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<object>();
                    return content!;
                }
                else
                {
                    throw new Exception("Erro ao buscar status da API");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }
        public async Task<GeoDtoResponse> GetGeoLocation(GeoDto geoDto)
        {
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://nominatim.openstreetmap.org/search?addressdetails=1&q={geoDto.Address}+{geoDto.City}+{geoDto.State}&format=jsonv2&limit=1");
                requestMessage.Headers.Add("Accept", "application/json");
                requestMessage.Headers.Add("User-Agent", "aspnet-user-agent");
                var response = await _client.SendAsync(requestMessage);
                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    var content = await JsonSerializer.DeserializeAsync<JsonElement[]>(responseStream);
                    if (content.Length > 0)
                    {
                        var geoResponse = new GeoDtoResponse
                        {
                            lat = content[0].GetProperty("lat").GetString(),
                            lon = content[0].GetProperty("lon").GetString()
                        };
                        return geoResponse;
                    }
                    else
                    {
                        throw new Exception("Endereço não encontrado");
                    }
                }
                else
                {
                    throw new Exception("Erro ao buscar localização");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<List<GeoDtoHotelResponse>> GetHotelsByGeo(GeoDto geoDto, IHotelRepository repository)
        {
            var location = await GetGeoLocation(geoDto);
            var hotels = repository.GetHotels().ToList();
            var response = new List<GeoDtoHotelResponse>();
            for (int i = 0; i < hotels.Count(); i++)
            {
                var hotelLocation = new GeoDto() {
                    Address = hotels[i].Address,
                    City = hotels[i].CityName,
                    State = hotels[i].State
                };
                var hotelGeo = await GetGeoLocation(hotelLocation);
                var distance = CalculateDistance(location.lat, location.lon, hotelGeo.lat, hotelGeo.lon);
                var hotelResponse = new GeoDtoHotelResponse
                {
                    HotelId = hotels[i].HotelId,
                    Name = hotels[i].Name,
                    Address = hotels[i].Address,
                    CityName = hotels[i].CityName,
                    State = hotels[i].State,
                    Distance = distance
                };
                response.Add(hotelResponse);
            }
            return response.OrderBy(h => h.Distance).ToList();
        }
       

        public int CalculateDistance (string latitudeOrigin, string longitudeOrigin, string latitudeDestiny, string longitudeDestiny) {
            double latOrigin = double.Parse(latitudeOrigin.Replace('.',','));
            double lonOrigin = double.Parse(longitudeOrigin.Replace('.',','));
            double latDestiny = double.Parse(latitudeDestiny.Replace('.',','));
            double lonDestiny = double.Parse(longitudeDestiny.Replace('.',','));
            double R = 6371;
            double dLat = radiano(latDestiny - latOrigin);
            double dLon = radiano(lonDestiny - lonOrigin);
            double a = Math.Sin(dLat/2) * Math.Sin(dLat/2) + Math.Cos(radiano(latOrigin)) * Math.Cos(radiano(latDestiny)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
            double distance = R * c;
            return int.Parse(Math.Round(distance,0).ToString());
        }

        public double radiano(double degree) {
            return degree * Math.PI / 180;
        }

    }
}