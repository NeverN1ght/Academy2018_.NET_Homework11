using AirportUWPClient.Abstractions;
using AirportUWPClient.Models;

namespace AirportUWPClient.Services
{
    public class StewardessesDataLoadService : BaseDataLoadService<Stewardesse>
    {
        public StewardessesDataLoadService() : base()
        {
            base.requestURL = "http://localhost:52898/api/stewardesses";
        }
    }
}
