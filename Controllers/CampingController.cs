using Microsoft.AspNetCore.Mvc;
using AirBnB_for_Campers___TAKE_HOME_EXAM.Models;
using System.Globalization;


namespace AirBnB_for_Campers___TAKE_HOME_EXAM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampingController : ControllerBase
    {
        private IDataContext _data;
        public CampingController(IDataContext data)
        {
            _data = data;
           

        }

        [HttpGet]
        public ActionResult<IEnumerable<Camping>> GetCampings(double? minPrice = null, double? maxPrice = null, string? availableFrom = null, string? availableTo = null, string? location = null)
        {
            // Fetch all campings
            var campings = _data.getCampings();

            // Apply price filter if provided
            if (minPrice.HasValue)
            {
                campings = campings.Where(c => c.PricePerNight >= minPrice);
            }
            if (maxPrice.HasValue)
            {
                campings = campings.Where(c => c.PricePerNight <= maxPrice);
            }

            // Apply date range filter if both fromDate and toDate are provided
            if (!string.IsNullOrEmpty(availableFrom) && !string.IsNullOrEmpty(availableTo))
            {
                // Validate date inputs
                DateTime fromDate, toDate;
                if (!DateTime.TryParseExact(availableFrom, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate) ||
                    !DateTime.TryParseExact(availableTo, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out toDate))
                {
                    return BadRequest("Invalid date format. Use dd-MM-yyyy format.");
                }

                // Filter campings based on available dates
                campings = campings.Where(c =>
                {
                    // Parse camping's available dates
                    DateTime campingFromDate, campingToDate;
                    if (!DateTime.TryParseExact(c.AvailableFrom, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out campingFromDate) ||
                        !DateTime.TryParseExact(c.AvailableTo, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out campingToDate))
                    {
                        // Handle invalid date formats in campings (optional)
                        return false;
                    }

                    // Check if the camping's available period encompasses the requested date range
                    return fromDate >= campingFromDate && toDate <= campingToDate;
                });
            }

            // Apply location filter if provided
            if (!string.IsNullOrEmpty(location))
            {
                string[] locations = location.Split(',').Select(x => x.Trim()).ToArray();
                campings = campings.Where(c => locations.Contains(c.Location));
            }

            return Ok(campings);
        }






        [HttpPost]
        public ActionResult AddCamping([FromBody] Camping camping)
        {
            // Validate and convert availableFrom
            if (!string.IsNullOrEmpty(camping.AvailableFrom) && !IsValidDateFormat(camping.AvailableFrom))
                return BadRequest("Invalid format for AvailableFrom. Use dd-MM-yyyy format.");

            // Validate and convert availableTo
            if (!string.IsNullOrEmpty(camping.AvailableTo) && !IsValidDateFormat(camping.AvailableTo))
                return BadRequest("Invalid format for AvailableTo. Use dd-MM-yyyy format.");

            _data.addCamping(camping);
            return Ok("Camping has been added!");
        }

        [HttpGet("{id}")]
        public ActionResult<Camping> Get(int id)
        {
            var camping = _data.getCampingById(id);
            if (camping == null)
            {
                return NotFound();
            }
            return Ok(camping);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCamping(int id)
        {
            var campingToDelete = _data.getCampingById(id);
            if (campingToDelete == null)
            {
                return NotFound();
            }

            _data.deleteCamping(id);
            return Ok("Camping has been deleted!");
        }

        // Helper method to validate date format using regex
        private bool IsValidDateFormat(string date)
        {
            try
            {
                DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


        // Add this method to CampingController.cs

        [HttpPut("{id}")]
        public ActionResult UpdateCamping(int id, [FromBody] Camping updatedCamping)
        {
            var existingCamping = _data.getCampingById(id);
            if (existingCamping == null)
            {
                return NotFound();
            }

            // Update existing camping properties
            existingCamping.Title = updatedCamping.Title;
            existingCamping.Location = updatedCamping.Location;
            existingCamping.ImageURL = updatedCamping.ImageURL;
            existingCamping.Description = updatedCamping.Description;
            existingCamping.AvailableFrom = updatedCamping.AvailableFrom;
            existingCamping.AvailableTo = updatedCamping.AvailableTo;
            existingCamping.PricePerNight = updatedCamping.PricePerNight;

            existingCamping.AvailableSpots = updatedCamping.AvailableSpots;

            // Save changes
            _data.updateCamping(existingCamping);

            return Ok("Camping has been updated!");
        }

      

    }


}
