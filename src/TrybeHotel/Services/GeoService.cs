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
                var address = Uri.EscapeDataString(geoDto.Address ?? string.Empty);
                var city = Uri.EscapeDataString(geoDto.City ?? string.Empty);
                var state = Uri.EscapeDataString(geoDto.State ?? string.Empty);

                var url = $"https://nominatim.openstreetmap.org/search?addressdetails=1&q={address},{city},{state}&format=jsonv2&limit=1";
                Console.WriteLine($"URL gerada: {url}");

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                requestMessage.Headers.Add("Accept", "application/json");
                requestMessage.Headers.Add("User-Agent", "aspnet-user-agent");
                var response = await _client.SendAsync(requestMessage);
                Console.WriteLine($"Status da resposta: {response.StatusCode}");
                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    var content = await JsonSerializer.DeserializeAsync<JsonElement[]>(responseStream);
                    Console.WriteLine($"Conteúdo da resposta: {JsonSerializer.Serialize(content)}");
                    if (content != null && content.Length > 0)
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
                    throw new Exception($"Erro ao buscar localização: {response.ReasonPhrase}");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Erro na obtenção de localização: {e.Message}");
            }
        }


        public async Task<List<GeoDtoHotelResponse>> GetHotelsByGeo(GeoDto geoDto, IHotelRepository repository)
        {
            var location = await GetGeoLocation(geoDto);
            var hotels = repository.GetHotels().ToList();
            var response = new List<GeoDtoHotelResponse>();
            for (int i = 0; i < hotels.Count(); i++)
            {
                var hotelLocation = new GeoDto()
                {
                    Address = hotels[i].Address,
                    City = hotels[i].CityName,
                    State = hotels[i].State
                };
                var hotelGeo = await GetGeoLocation(hotelLocation);
                var distance = CalculateDistance(location.lat!, location.lon!, hotelGeo.lat!, hotelGeo.lon!);
                var hotelResponse = new GeoDtoHotelResponse
                {
                    HotelId = hotels[i].HotelId,
                    Name = hotels[i].Name,
                    Address = hotels[i].Address,
                    Image = hotels[i].Image,
                    CityName = hotels[i].CityName,
                    State = hotels[i].State,
                    Distance = distance
                };
                response.Add(hotelResponse);
            }
            return response.OrderBy(h => h.Distance).ToList();
        }

        public async Task<List<RoomDtoResponse>> GetRoomsByGeo(GeoDto geoDto, IRoomRepository repository)
        {
            var location = await GetGeoLocation(geoDto);
            var rooms = repository.GetAllRooms().ToList();
            var response = new List<RoomDtoResponse>();
            for (int i = 0; i < rooms.Count(); i++)
            {
                var roomLocation = new GeoDto()
                {
                    Address = rooms[i].Hotel!.Address,
                    City = rooms[i].Hotel!.CityName,
                    State = rooms[i].Hotel!.State
                };
                var roomGeo = await GetGeoLocation(roomLocation);
                var distance = CalculateDistance(location.lat!, location.lon!, roomGeo.lat!, roomGeo.lon!);
                var roomResponse = new RoomDtoResponse
                {
                    RoomId = rooms[i].RoomId,
                    Name = rooms[i].Name,
                    Capacity = rooms[i].Capacity,
                    Image = rooms[i].Image,
                    Hotel = rooms[i].Hotel,
                    Distance = distance
                };
                response.Add(roomResponse);
            }
            return response.OrderBy(r => r.Distance).ToList();
        }


        public int CalculateDistance(string latitudeOrigin, string longitudeOrigin, string latitudeDestiny, string longitudeDestiny)
        {
            double latOrigin = double.Parse(latitudeOrigin.Replace('.', ','));
            double lonOrigin = double.Parse(longitudeOrigin.Replace('.', ','));
            double latDestiny = double.Parse(latitudeDestiny.Replace('.', ','));
            double lonDestiny = double.Parse(longitudeDestiny.Replace('.', ','));
            double R = 6371;
            double dLat = radiano(latDestiny - latOrigin);
            double dLon = radiano(lonDestiny - lonOrigin);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(radiano(latOrigin)) * Math.Cos(radiano(latDestiny)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;
            return int.Parse(Math.Round(distance, 0).ToString());
        }

        public double radiano(double degree)
        {
            return degree * Math.PI / 180;
        }

    }
}