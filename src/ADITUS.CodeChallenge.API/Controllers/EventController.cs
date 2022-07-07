using ADITUS.CodeChallenge.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ADITUS.CodeChallenge.API
{
  public class EventController : ControllerBase
  {
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
      _eventService = eventService;
    }

    [HttpGet]
    [Route("events/{id}")]
    public async Task<IActionResult> GetEvent(Guid id)
    {
      var @event = await _eventService.GetEvent(id);
      if (@event == null)
      {
        return NotFound();
      }

      return Ok(@event);
    }

    [HttpGet]
    [Route("events")]
    public async Task<IActionResult> GetEvents()
    {
      var events = await _eventService.GetEvents();
      return Ok(events);
    }
  }
}