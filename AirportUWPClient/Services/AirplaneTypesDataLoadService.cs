using AirportUWPClient.Abstractions;
using AirportUWPClient.Models;

namespace AirportUWPClient.Services
{
    public class AirplaneTypesDataLoadService : BaseDataLoadService<AirplaneType>
    {
        public AirplaneTypesDataLoadService() : base()
        {
            base.requestURL = "http://localhost:52898/api/airplanetypes";
        }
    }
}
