using System.ComponentModel;
using ADITUS.CodeChallenge.API.Domain;
using ADITUS.CodeChallenge.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ADITUS.CodeChallenge.API
{
  [Route("reservation")]
  public class HardwareController : ControllerBase
  {
    //Service-Injection
    private readonly IHardwareService _hardwareService;
    private readonly IEventHarwdareService _eventHardwareService;
    private readonly IReservationService _reservationService; 

    public HardwareController(IHardwareService hardwareService, IEventHarwdareService eventHardwareService, IReservationService reservationService)
    {
      //Services initialisieren
      _hardwareService = hardwareService;
      _eventHardwareService = eventHardwareService;
      _reservationService = reservationService; 
    }

    [HttpGet]
    [Route("")]
    //Holt alle HardwareComponents
    public async Task<IActionResult> GetComponents()
    {
      var components = await _hardwareService.GetComponents();
      return Ok(components);
    }

    [HttpPost]
    [Route("reserve/{eventId}")]
    //Methode, um ein Hardware-Component für ein Event zu reservieren
    public async Task<IActionResult> ReserveComponentForEvent(Guid eventId, [FromBody] ReservationRequest request)
    {
      try
      {
        var success = await _reservationService.ReserveHardwareComponent(
            eventId,
            request.EventName,
            request.HardwareIds,
            request.Quantities,
            request.StartDate,
            request.EndDate
        );

        if (!success)
        {
          return BadRequest("Die Reservierung konnte nicht abgeschlossen werden.");
        }
        return Ok(new { message = "Die Reservierung wurde erfolgreich durchgeführt. Eine Freigabe wird bearbeitet. Bitte warten Sie auf einer Rückmeldung. Das Prozess kann dauern."});
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine($"Fehler bei der Reservierung: {ex.Message}");
        return StatusCode(500, "Interne Fehler. Bitte wenden Sie sich bei der Support.");
      }
    }

    [HttpGet]
    [Route("reservations")]
    //Holt alle Reservierungen
    public async Task<IActionResult> GetAllReservations()
    {
      try
      {
        var reservations = await _reservationService.GetAllReservations();
        return Ok(reservations);
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine($"Fehler bei bekommen der Reservierung: {ex.Message}");
        return StatusCode(500, "Interne Fehler. Bitte wenden Sie sich bei der Support.");
      }
    }
  }
}
