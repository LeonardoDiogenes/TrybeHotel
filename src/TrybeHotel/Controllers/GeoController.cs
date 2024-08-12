using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using TrybeHotel.Dto;
using TrybeHotel.Services;
using System.Diagnostics;


namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("geo")]
    public class GeoController : Controller
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IGeoService _geoService;

        private readonly IRoomRepository _roomRepository;


        public GeoController(IHotelRepository hotelRepository, IGeoService geoService, IRoomRepository roomRepository)
        {
            _hotelRepository = hotelRepository;
            _geoService = geoService;
            _roomRepository = roomRepository;
        }

        [HttpGet]
        [Route("status")]
        public async Task<IActionResult> GetStatus()
        {
            try
            {
                var response = await _geoService.GetGeoStatus();
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("hotel/address")]
        public async Task<IActionResult> GetHotelsByLocation([FromBody] GeoDto address)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var response = await _geoService.GetHotelsByGeo(address, _hotelRepository);
                stopwatch.Stop();
                Console.WriteLine($"Tempo de execução: {stopwatch.ElapsedMilliseconds}ms");
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("room/address")]
        public async Task<IActionResult> GetRoomsByLocation([FromBody] GeoDto address)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var response = await _geoService.GetRoomsByGeo(address, _roomRepository);
                stopwatch.Stop();
                Console.WriteLine($"Tempo de execução: {stopwatch.ElapsedMilliseconds}ms");
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("testGeo")]
        public async Task<IActionResult> TestGeo([FromBody] GeoDto address)
        {
            try
            {
                var response = await _geoService.GetGeoLocation(address);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }


}