using Microsoft.AspNetCore.Mvc;
using AirBnB_for_Campers___TAKE_HOME_EXAM.Models;
using System.Globalization;

namespace AirBnB_for_Campers___TAKE_HOME_EXAM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private IDataContext _data;

        public BookingController(IDataContext data)
        {
            _data = data;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Booking>> GetBookings()
        {
            return Ok(_data.getBookings());
        }

        [HttpPost]
        public ActionResult AddBooking([FromBody] Booking booking)
        {
            if (!string.IsNullOrEmpty(booking.Date) && !IsValidDateFormat(booking.Date))
                return BadRequest("Invalid format for Date. Use dd/MM/yyyy format.");

            _data.addBooking(booking);
            return Ok("Booking has been added!");
        }

        private bool IsValidDateFormat(string date)
        {
            try
            {
                DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Booking> GetBookingById(int id)
        {
            return Ok(_data.getBookingById(id));
        }

        [HttpGet("User/{userId}")]
        public ActionResult<Booking> GetBookingsByUser(int userId)
        {
            return Ok(_data.getBookingsFromUser(userId));
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBooking(int id)
        {
            var bookingToDelete = _data.getBookingById(id);
            if (bookingToDelete == null)
            {
                return NotFound();
            }

            _data.deleteBooking(id);
            return Ok("Booking has been deleted!");
        }
    }
}
