using ADITUS.CodeChallenge.API.Domain;
using ADITUS.CodeChallenge.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ADITUS.CodeChallenge.API
{
  [Route("statistics")]
  [ApiController]

  public class StatisticsController : ControllerBase
  {
    private readonly IEventService _eventService;

    public StatisticsController(IEventService eventService)
    {
      _eventService = eventService;
    }

    [HttpGet]
    [Route("")]
    //Holt alle Events
    public async Task<ActionResult<IEnumerable<Event>>> GetEvents() 
    {
      var (eventOnsite, eventOnline, eventHybrid) = await _eventService.GetEventsWithStatistics();
      var myEvents = await _eventService.GetEvents();

      return Ok(new
      {
        EventOnsite = eventOnsite,
        EventOnline = eventOnline,
        EventHybrid = eventHybrid,
        MyEvents = myEvents
      });
    }
  }
}
