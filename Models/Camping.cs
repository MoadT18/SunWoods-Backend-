// Models/Camping.cs

using System;

namespace AirBnB_for_Campers___TAKE_HOME_EXAM.Models
{
    public class Camping
    {
        public int ID { get; private set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public string? AvailableFrom { get; set; } // Changed to string
        public string? AvailableTo { get; set; }   // Changed to string
        public double PricePerNight { get; set; }
        public int AvailableSpots { get; set; }
    }

}
