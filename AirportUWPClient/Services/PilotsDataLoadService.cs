using AirportUWPClient.Abstractions;
using AirportUWPClient.Models;

namespace AirportUWPClient.Services
{
    public class PilotsDataLoadService : BaseDataLoadService<Pilot>
    {
        public PilotsDataLoadService() : base()
        {
            base.requestURL = "http://localhost:52898/api/pilots";
        }
    }
}
