namespace AirBnB_for_Campers___TAKE_HOME_EXAM.Models
{
    public class Rating
    {
        public int ID { get; private set; }
        public int UserId { get; set; }
        public int CampingId { get; set; }

        public string Comment { get; set; }
        public int Rate { get; set; }
        public string ?Date { get; set; }


    }
}
