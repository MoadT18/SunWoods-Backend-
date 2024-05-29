using Microsoft.AspNetCore.Mvc;
using AirBnB_for_Campers___TAKE_HOME_EXAM.Models;
using System.Globalization;

namespace AirBnB_for_Campers___TAKE_HOME_EXAM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingController : ControllerBase
    {
        private IDataContext _data;

        public RatingController(IDataContext data)
        {
            _data = data;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Rating>> GetRatings()
        {
            return Ok(_data.getRatings());
        }

        [HttpPost]
        public ActionResult AddRatings([FromBody] Rating rating)
        {
            if (!string.IsNullOrEmpty(rating.Date) && !IsValidDateFormat(rating.Date))
                return BadRequest("Invalid format for Date. Use dd/MM/yyyy format.");

            _data.addRating(rating);
            return Ok("Rating has been added!");
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
        public ActionResult<Rating> GetRatingById(int id)
        {
            return Ok(_data.getRatingById(id));
        }

        [HttpGet("User/{userId}")]
        public ActionResult<Rating> GetRatingsByUser(int userId)
        {
            return Ok(_data.getRatingsFromUser(userId));
        }



        [HttpGet("Camping/{campingId}")]
        public ActionResult<Rating> GetRatingsByCamping(int campingId)
        {
            return Ok(_data.getRatingsFromCamping(campingId));
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRating(int id)
        {
            var ratingToDelete = _data.getRatingById(id);
            if (ratingToDelete == null)
            {
                return NotFound();
            }

            _data.deleteRating(id);
            return Ok("Rating has been deleted!");
        }

        [HttpDelete]
        public ActionResult DeleteRatings()
        {
         

            _data.deleteRatings();
            return Ok("Ratings has been deleted!");
        }
        [HttpGet("Top3Ratings")]
        public ActionResult<IEnumerable<Rating>> GetTop3Ratings()
        {
            return Ok(_data.GetTop3Ratings());
        }
    }
}
