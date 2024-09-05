using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TrybeHotel.Dto;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("booking")]
  
    public class BookingController : Controller
    {
        private readonly IBookingRepository _repository;
        public BookingController(IBookingRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Client")]
        public IActionResult Add([FromBody] BookingDtoInsert bookingInsert){
            try
            {
                var token = HttpContext.User.Identity as ClaimsIdentity;
                var userEmail = token?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var booking = _repository.Add(bookingInsert, userEmail!);
                return Created("", booking);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }


        [HttpGet("{Bookingid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Client")]
        public IActionResult GetBooking(int bookingId){
            try
            {
                var token = HttpContext.User.Identity as ClaimsIdentity;
                var userEmail = token?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                var booking = _repository.GetBooking(bookingId, userEmail!);

                if (booking != null && userEmail != null)
                {
                    return Ok(booking);
                }
                return Unauthorized();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Client")]
        public IActionResult GetBookings(){
            try
            {
                var token = HttpContext.User.Identity as ClaimsIdentity;
                var userEmail = token?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var bookings = _repository.GetBookings(userEmail!);
                return Ok(bookings);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut("{Bookingid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Client")]
        public IActionResult UpdateBooking(int bookingId, [FromBody] BookingDtoInsert bookingInsert){
            try
            {
                var token = HttpContext.User.Identity as ClaimsIdentity;
                var userEmail = token?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var booking = _repository.UpdateBooking(bookingId, bookingInsert, userEmail!);
                return Ok(booking);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpDelete("{Bookingid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Client")]
        public IActionResult DeleteBooking(int bookingId){
            try
            {
                _repository.DeleteBooking(bookingId);
                return NoContent();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}