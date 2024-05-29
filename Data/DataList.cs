using AirBnB_for_Campers___TAKE_HOME_EXAM.Models;
using System.Globalization;

public class DataList : IDataContext
    {

    List<User> users = new List<User>();
    List<Booking> bookings = new List<Booking>();

    List<Camping> campings = new List<Camping>();

    List<Rating> ratings = new List<Rating>();


    // USER

    public void addUser(User user)
    {
        users.Add(user);
    }

    public IEnumerable<User> getUsers() {
    
            return users;
    }

    

    public void deleteUser(int id)
    {
        var userID = getUserById(id);
        if (userID != null)
        {
            users.Remove(userID);
        }
    }



    public User getUserById(int id)
    {
        return users.FirstOrDefault(u => u.ID == id);

    }


    public void updateUser(User updatedUser)
    {
        var existingUser = users.FirstOrDefault(u => u.ID == updatedUser.ID);
        if (existingUser != null)
        {
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.UserName = updatedUser.UserName;
            existingUser.Email = updatedUser.Email;
            existingUser.HashedPassword = updatedUser.HashedPassword; // Update hashed password
        }
    }


    // BOOKING
    public IEnumerable<Booking> getBookings()
    {

        return bookings;
    }

    public void addBooking(Booking booking)
    {
        bookings.Add(booking);
    }

    public Booking getBookingById(int id)
    {
        return bookings.FirstOrDefault(b => b.ID == id);

    }
    public IEnumerable<Booking> getBookingsFromUser(int userId)
    {
        return bookings.Where(b => b.UserId == userId);
    }

    public void deleteBooking(int id)
    {
        var bookingToRemove = bookings.FirstOrDefault(b => b.ID == id);
        if (bookingToRemove != null)
        {
            bookings.Remove(bookingToRemove);
        }
    }

    // CAMPING

    public void addCamping(Camping camping)
    {
        campings.Add(camping);
    }

    public IEnumerable<Camping> getCampings()
    {

        return campings;
    }


    public Camping getCampingById(int id)
    {
        return campings.FirstOrDefault(c => c.ID == id);

    }


    public void deleteCamping(int id)
    {
        var campingToRemove = campings.FirstOrDefault(c => c.ID == id);
        if (campingToRemove != null)
        {
            campings.Remove(campingToRemove);
        }
    }

    public void updateCamping(Camping camping)
    {
        throw new NotImplementedException();
    }

    //RATING

    public IEnumerable<Rating> getRatings()
    {

        return ratings;
    }

    public void addRating(Rating rating)
    {
        ratings.Add(rating);
    }

    public Rating getRatingById(int id)
    {
        return ratings.FirstOrDefault(r => r.ID == id);

    }
    public IEnumerable<Rating> getRatingsFromUser(int userId)
    {
        return ratings.Where(r => r.UserId == userId);
    }

    public IEnumerable<Rating> getRatingsFromCamping(int campingId)
    {
        return ratings.Where(r => r.CampingId == campingId);
    }

    public void deleteRating(int id)
    {
        var ratingToRemove = ratings.FirstOrDefault(r => r.ID == id);
        if (ratingToRemove != null)
        {
            ratings.Remove(ratingToRemove);
        }
    }
    public void deleteRatings()
    {
        ratings.Clear();
    }
    public IEnumerable<Rating> GetTop3Ratings()
    {
        return ratings.OrderByDescending(r => r.Rate)
                      .ThenByDescending(r => DateTime.ParseExact(r.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                      .Take(3)
                      .ToList();
    }

}

