using BookingSystemApi.Data;
using BookingSystemApi.Models;
using Microsoft.AspNetCore.Mvc;
namespace BookingSystemApi.Controllers
{
    [ApiController]
    [Route("/api[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly appDbContext _context;
        
        public BookingController(appDbContext booking)
        {
            _context = booking;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Bookings.ToList());
        }


        [HttpGet("avilable")]
        public IActionResult GetAll(DateTime date)
        {
            var avilable = _context.Bookings.Any(x => x.Date == date);
                return Ok(!avilable);
        }


        [HttpPost]
        public IActionResult NewBooking(CreatBookingDto dto)
        {
            var exist = _context.Bookings.Any(x => x.Date == dto.Date);
            if (exist)
            {
                return BadRequest();
            }
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Enter Name");

            if (dto.Date < DateTime.Now)
                return BadRequest("Enter correct Date");

            var booking = new Booking
            {
                Name = dto.Name,
                Date = dto.Date,
            };
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return Ok(booking);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(x => x.Id == id);
            if (booking == null)
            {
                return NotFound();
            }
            _context.Bookings.Remove(booking);
            _context.SaveChanges();

            return Ok();
        }
       
    }
}
