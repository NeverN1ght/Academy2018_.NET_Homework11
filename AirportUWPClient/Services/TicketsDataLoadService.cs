using AirportUWPClient.Abstractions;
using AirportUWPClient.Models;

namespace AirportUWPClient.Services
{
    public class TicketsDataLoadService : BaseDataLoadService<Ticket>
    {
        public TicketsDataLoadService() : base()
        {
            base.requestURL = "http://localhost:52898/api/tickets";
        }
    }
}
