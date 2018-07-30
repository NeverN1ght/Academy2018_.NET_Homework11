using AirportUWPClient.Abstractions;
using AirportUWPClient.Models;

namespace AirportUWPClient.Services
{
    public class AirplanesDataLoadService : BaseDataLoadService<Airplane>
    {
        public AirplanesDataLoadService() : base()
        {
            base.requestURL = "http://localhost:52898/api/airplanes";
        }
    }
}
