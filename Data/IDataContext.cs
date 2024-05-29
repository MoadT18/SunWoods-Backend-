using AirBnB_for_Campers___TAKE_HOME_EXAM.Models;


public interface IDataContext
{
    // USER
    void addUser(User user);

    void deleteUser(int id);

    IEnumerable<User> getUsers();
    User getUserById(int id);


    void updateUser(User user);
    //BOOKING

    void addBooking(Booking booking);
    IEnumerable<Booking> getBookings();

    Booking getBookingById(int id);

    void deleteBooking(int id);


    IEnumerable<Booking> getBookingsFromUser(int userId);

    // CAMPING

    void addCamping(Camping camping);

    IEnumerable<Camping> getCampings();

    Camping getCampingById(int id);

    void deleteCamping(int id);

    void updateCamping(Camping camping);


    //RATING

    void addRating(Rating rating);
    IEnumerable<Rating> getRatings();

    Rating getRatingById(int id);

    void deleteRating(int id);

    IEnumerable<Rating> getRatingsFromUser(int userId);

    IEnumerable<Rating> getRatingsFromCamping(int campingId);

    public void deleteRatings();
    IEnumerable<Rating> GetTop3Ratings();



}

