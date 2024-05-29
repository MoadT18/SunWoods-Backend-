using Microsoft.AspNetCore.Mvc;

namespace AirBnB_for_Campers___TAKE_HOME_EXAM.Models
{
    public class Booking
    {
        public int ID { get; private set; }

        public int UserId { get; set; }
        public int CampsiteId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public int TotalNights { get; set; }

        public double TotalPrice { get; set; }

        public string ?Date { get; set; }



    }
}
