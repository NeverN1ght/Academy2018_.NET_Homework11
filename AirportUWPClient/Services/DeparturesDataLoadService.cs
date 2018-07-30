using AirportUWPClient.Abstractions;
using AirportUWPClient.Models;

namespace AirportUWPClient.Services
{
    public class DeparturesDataLoadService : BaseDataLoadService<Departure>
    {
        public DeparturesDataLoadService() : base()
        {
            base.requestURL = "http://localhost:52898/api/departures";
        }
    }
}
