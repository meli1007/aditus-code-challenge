using ADITUS.CodeChallenge.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ADITUS.CodeChallenge.API
{
  [Route("events")]
  public class EventsController : ControllerBase
  {
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
      _eventService = eventService;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetEvents()
    {
      var events = await _eventService.GetEvents();
      return Ok(events);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetEvent(Guid id)
    {
      var @event = await _eventService.GetEvent(id);
      if (@event == null)
      {
        return NotFound();
      }

      return Ok(@event);
    }
  }
}