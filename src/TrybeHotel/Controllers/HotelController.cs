using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using TrybeHotel.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("hotel")]
    public class HotelController : Controller
    {
        private readonly IHotelRepository _repository;

        public HotelController(IHotelRepository repository)
        {
            _repository = repository;
        }
        
        // 4. Desenvolva o endpoint GET /hotel
        [HttpGet]
        public IActionResult GetHotels(){
            return Ok(_repository.GetHotels());
        }

        // 5. Desenvolva o endpoint POST /hotel
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Admin")]
        public IActionResult PostHotel([FromBody] Hotel hotel){
            return Created("" ,_repository.AddHotel(hotel));
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Admin")]
        public IActionResult DeleteHotel(int id){
            try
            {
                _repository.DeleteHotel(id);
                return NoContent();
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Admin")]
        public IActionResult PutHotel([FromBody] Hotel hotel){
            try
            {
                return Ok(_repository.UpdateHotel(hotel));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
}
}