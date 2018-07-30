using System;

namespace AirportUWPClient.Models
{
    public class Pilot
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        public int Experience { get; set; }
    }
}
