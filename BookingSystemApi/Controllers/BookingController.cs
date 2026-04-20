using BookingSystemApi.Models;
using Microsoft.AspNetCore.Mvc;
namespace BookingSystemApi.Controllers
{
    [ApiController]
    [Route("/api[controller]")]
    public class BookingController : ControllerBase
    {
        private static List<Booking> _booking = new List<Booking>();  
        private static int NextID = 1;
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_booking);
        }
        [HttpGet("avilable")]
        public IActionResult GetAll(DateTime date)
        {
            var avilable = _booking.Any(x => x.Date == date);
                return Ok(!avilable);
        }
        [HttpPost]
        public IActionResult NewBooking(Booking booking)
        {
            var exist = _booking.Any(x => x.Date == booking.Date);
            if (exist)
            {
                return BadRequest();
            }
            if (string.IsNullOrWhiteSpace(booking.Name))
                return BadRequest("Enter Name");

            if (booking.Date < DateTime.Now)
                return BadRequest("Enter correct Date");

            booking.Id = NextID++;
            _booking.Add(booking);
            return Ok(booking);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var booking = _booking.FirstOrDefault(x => x.Id == id);
            if (booking == null)
            {
                return NotFound();
            }
            _booking.Remove(booking);
            return Ok();
        }
       
    }
}
