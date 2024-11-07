using ADITUS.CodeChallenge.API.Domain;

namespace ADITUS.CodeChallenge.API.Services
{
  public interface IEventService
  {
    //Holt ein Event je nach Id
    Task<Event> GetEvent(Guid id);
    //Holt alle Events
    Task<IList<Event>> GetEvents();

    Task<(List<EventOnsite> eventOnsite, List<EventOnline> eventOnline, List<EventHybrid> eventHybrid)> GetEventsWithStatistics();
  }
}