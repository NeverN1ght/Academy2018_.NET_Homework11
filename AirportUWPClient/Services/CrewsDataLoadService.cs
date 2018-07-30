using AirportUWPClient.Abstractions;
using AirportUWPClient.Models;

namespace AirportUWPClient.Services
{
    public class CrewsDataLoadService : BaseDataLoadService<Crew>
    {
        public CrewsDataLoadService() : base()
        {
            base.requestURL = "http://localhost:52898/api/crews";
        }
    }
}
