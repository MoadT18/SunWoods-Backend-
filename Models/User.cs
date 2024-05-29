using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters; // Import Newtonsoft.Json for JSON serialization

namespace AirBnB_for_Campers___TAKE_HOME_EXAM.Models
{
 
    public class User
    {
        public int ID { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public int Roles { get; set; }



    }
}
