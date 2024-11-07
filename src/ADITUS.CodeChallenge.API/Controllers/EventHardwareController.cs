using ADITUS.CodeChallenge.API.Domain;
using ADITUS.CodeChallenge.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ADITUS.CodeChallenge.API.Controllers
{
  [Route("eventhardware")]
  public class EventHardwareController : ControllerBase
  {
    private readonly IEventHarwdareService _eventHardwareService;

    public EventHardwareController(IEventHarwdareService eventHardwareService)
    {
      _eventHardwareService = eventHardwareService;
    }

    [HttpGet]
    [Route("")]
    //Holt alle Statische Events
    public async Task<IActionResult> GetAllEvents()
    {
      var events = await _eventHardwareService.GetAllEvents();
      return Ok(events);
    }

    [HttpGet]
    [Route("{id}")]
    //Holt alle Statische Events nach Id
    public async Task<IActionResult> GetEvent(Guid id)
    {
      var @event = await _eventHardwareService.GetEvent(id);
      if (@event == null)
      {
        return NotFound();
      }
      return Ok(@event);
    }
  }
}
