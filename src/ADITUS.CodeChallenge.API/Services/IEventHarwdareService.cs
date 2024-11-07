using ADITUS.CodeChallenge.API.Domain;
namespace ADITUS.CodeChallenge.API.Services
{
  public interface IEventHarwdareService
  {
    //Holt alle reservierte Hardware-Components von einem spezifische Event 
    Task<EventWithHardware> GetReservedComponentForMyEvent(Guid eventId, Guid componentId);
    //Holt Event je nach Id
    Task<EventWithHardware> GetEvent(Guid id);
    //Holt alle Events
    Task<IList<EventWithHardware>> GetAllEvents();
    //Hardware-Component zur Event hinzufügen
    Task<bool> ReserveComponentForEventAsync(Guid eventId, Guid componentId, DateTime startDate);
  }
}
