using LiteDB;
using AirBnB_for_Campers___TAKE_HOME_EXAM.Models;
using System;
using System.Globalization;

public class Database : IDataContext, IDisposable
{
    private LiteDatabase db;

    public Database()
    {
        db = new LiteDatabase(@"campingData.db");
    }

    // USER
    public void addUser(User user)
    {
        db.BeginTrans();
        try
        {
            db.GetCollection<User>("Users").Insert(user);
            db.Commit();
        }
        catch
        {
            db.Rollback();
            throw;
        }
    }
    public User getUserById(int id)
    {
        return db.GetCollection<User>("Users").FindById(id);
    }

    public IEnumerable<User> getUsers()
    {
        return db.GetCollection<User>("Users").FindAll();
    }

    public void deleteUser(int id)
    {
        db.BeginTrans();
        try
        {
            db.GetCollection<User>("Users").Delete(id);
            db.Commit();
        }
        catch
        {
            db.Rollback();
            throw;
        }
    }

    public void updateUser(User updatedUser)
    {
        db.BeginTrans();
        try
        {
            db.GetCollection<User>("Users").Update(updatedUser);
            db.Commit();
        }
        catch
        {
            db.Rollback();
            throw;
        }
    }

    // BOOKING

    public void addBooking(Booking booking)
    {
        db.BeginTrans();
        try
        {
            db.GetCollection<Booking>("Bookings").Insert(booking);
            db.Commit();
        }
        catch
        {
            db.Rollback();
            throw;
        }

    }

    public IEnumerable<Booking> getBookings()
    {
        return db.GetCollection<Booking>("Bookings").FindAll();
    }

    public Booking getBookingById(int id)
    {
        return db.GetCollection<Booking>("Bookings").FindById(id);
    }
    public IEnumerable<Booking> getBookingsFromUser(int userId)
    {
        var bookings = db.GetCollection<Booking>("Bookings")
                         .Find(x => x.UserId == userId)
                         .ToList();

        return bookings;
    }

    public void deleteBooking(int id)
    {
        db.BeginTrans();
        try
        {
            db.GetCollection<Booking>("Bookings").Delete(id);
            db.Commit();
        }
        catch
        {
            db.Rollback();
            throw;
        }

    }

    // CAMPING

    public void addCamping(Camping camping)
    {
        db.BeginTrans();
        try
        {
            db.GetCollection<Camping>("Campings").Insert(camping);
            db.Commit();
        }
        catch
        {
            db.Rollback();
            throw;
        }

    }

    public IEnumerable<Camping> getCampings()
    {
        return db.GetCollection<Camping>("Campings").FindAll();
    }

    public Camping getCampingById(int id)
    {
        return db.GetCollection<Camping>("Campings").FindById(id);
    }

    public void deleteCamping(int id)
    {
        db.BeginTrans();
        try
        {
            db.GetCollection<Camping>("Campings").Delete(id);
            db.Commit();
        }
        catch
        {
            db.Rollback();
            throw;
        }
    }

    public void updateCamping(Camping camping)
    {

        db.BeginTrans();
        try
        {
            db.GetCollection<Camping>("Campings").Update(camping);
            db.Commit();
        }
        catch
        {
            db.Rollback();
            throw;
        }
    }

    //RATING

    public void addRating(Rating rating)
    {
        db.BeginTrans();
        try
        {
            db.GetCollection<Rating>("Ratings").Insert(rating);
            db.Commit();
        }
        catch
        {
            db.Rollback();
            throw;
        }

    }

    public IEnumerable<Rating> getRatings()
    {
        return db.GetCollection<Rating>("Ratings").FindAll();
    }

    public Rating getRatingById(int id)
    {
        return db.GetCollection<Rating>("Ratings").FindById(id);
    }
    public IEnumerable<Rating> getRatingsFromUser(int userId)
    {
        var ratings = db.GetCollection<Rating>("Ratings")
                         .Find(x => x.UserId == userId)
                         .ToList();

        return ratings;
    }

    public IEnumerable<Rating> getRatingsFromCamping(int campingId)
    {
        var ratings = db.GetCollection<Rating>("Ratings")
                         .Find(x => x.CampingId == campingId)
                         .ToList();

        return ratings;
    }
    public void deleteRating(int id)
    {
        db.BeginTrans();
        try
        {
            db.GetCollection<Rating>("Ratings").Delete(id);
            db.Commit();
        }
        catch
        {
            db.Rollback();
            throw;
        }

    }

    public void deleteRatings()
    {
        db.BeginTrans();
        try
        {
            db.GetCollection<Rating>("Ratings").DeleteAll();
            db.Commit();
        }
        catch
        {
            db.Rollback();
            throw;
        }

    }
    public IEnumerable<Rating> GetTop3Ratings()
    {
        var ratings = db.GetCollection<Rating>("Ratings")
                        .FindAll()
                        .OrderByDescending(r => r.Rate)
                        .ThenByDescending(r => DateTime.ParseExact(r.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                        .Take(3)
                        .ToList();
        return ratings;
    }
    public void Dispose()
    {
        db.Dispose();
    }

   
}
