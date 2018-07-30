using System;
using System.Collections.Generic;

namespace AirportUWPClient.Models
{
    public class Flight
    {
        public string Number { get; set; }

        public string DeparturePoint { get; set; }

        public string DestinationPoint { get; set; }

        public DateTime ArrivalTime { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}
