using ADITUS.CodeChallenge.API.Domain;
namespace ADITUS.CodeChallenge.API.Services
{
  public class EventHardwareService : IEventHarwdareService
  {
    private readonly IList<EventWithHardware> _eventsWithHardware;
    private readonly IHardwareService _hardwareService;
   
    public EventHardwareService(IReservationService reservationService, IHardwareService hardwareService)
    {
      _hardwareService = hardwareService;
      // statische Events für die Hardware-Reservierung 
      _eventsWithHardware = new List<EventWithHardware>
      {
        new EventWithHardware
        {
          Id = Guid.Parse("c3f3e3ef-d55d-46ef-a6d2-de78ec1cbe82"),
          Name = "Auto Events",
          StartDate = new DateTime(2024, 12, 10),
          EndDate = new DateTime(2024, 12, 12)

        },
        new EventWithHardware
        {
          Id = Guid.Parse("a1fc1cd4-31c9-43eb-8676-9e8cc3a8963b"),
          Name = "EnergyDecentral",
          StartDate = new DateTime(2024, 11, 29),
          EndDate = new DateTime(2024, 11, 30)
        },
        new EventWithHardware
        {
          Id = Guid.Parse("7eb60298-3008-493d-b59b-8908a7cd4b73"),
          Name = "Konzert 1",
          StartDate = new DateTime(2024, 12, 20),
          EndDate = new DateTime(2024, 12, 20)
        },
        new EventWithHardware
        {
          Id = Guid.Parse("25775d20-5520-46a7-a66f-217d9f72977b"),
          Name = "Konzert 2",
          StartDate = new DateTime(2024, 12, 20),
          EndDate = new DateTime(2024, 12, 20)
        },
      };
    }

    public Task<IList<EventWithHardware>> GetAllEvents()
    {
      return Task.FromResult(_eventsWithHardware);
    }

    public Task<EventWithHardware> GetEvent(Guid id) 
    {
      var @event = _eventsWithHardware.FirstOrDefault(e => e.Id == id);
      return Task.FromResult(@event);
    }
    public Task<EventWithHardware> GetReservedComponentForMyEvent(Guid eventId, Guid componentId)
    {
      throw new NotImplementedException();
    }

    public Task<bool> ReserveComponentForEventAsync(Guid eventId, Guid componentId, DateTime startDate)
    {
      throw new NotImplementedException();
    }
  }
}
