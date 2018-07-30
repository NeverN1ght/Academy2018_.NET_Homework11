using AirportUWPClient.Abstractions;
using AirportUWPClient.Models;

namespace AirportUWPClient.Services
{
    public class FlightsDataLoadService : BaseDataLoadService<Flight>
    {
        public FlightsDataLoadService() : base()
        {
            base.requestURL = "http://localhost:52898/api/flights";
        }
    }
}
