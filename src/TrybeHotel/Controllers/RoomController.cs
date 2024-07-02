using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("room")]
    public class RoomController : Controller
    {
        private readonly IRoomRepository _repository;
        public RoomController(IRoomRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{HotelId}")]
        public IActionResult GetRoom(int HotelId){
            return Ok(_repository.GetRooms(HotelId));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Admin")]
        public IActionResult PostRoom([FromBody] Room room){
            return Created("", _repository.AddRoom(room));
        }

        [HttpDelete("{RoomId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Admin")]
        public IActionResult Delete(int RoomId)
        {
            _repository.DeleteRoom(RoomId);
            return NoContent();
        }

        [HttpPut("{RoomId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Admin")]
        public IActionResult UpdateRoom(int RoomId, [FromBody] Room room)
        {
            try
            {
                return Ok(_repository.UpdateRoom(RoomId, room));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}