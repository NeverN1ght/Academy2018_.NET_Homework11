using System.Collections.Generic;

namespace AirportUWPClient.Models
{
    public class Crew
    {
        public int Id { get; set; }

        public Pilot Pilot { get; set; }

        public List<Stewardesse> Stewardesses { get; set; }
    }
}
