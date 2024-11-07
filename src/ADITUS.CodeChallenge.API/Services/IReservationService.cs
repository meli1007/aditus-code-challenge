using ADITUS.CodeChallenge.API.Domain;

namespace ADITUS.CodeChallenge.API.Services
{
  public interface IReservationService
  {
    //Reservierung einer spezifische Komponente für eine Event
    Task<bool> ReserveHardwareComponent(Guid eventId, string eventName, List<Guid> hardwareIds, List<int> quantities, DateTime startDate, DateTime endDate);
    //püft die Verfügbarkeit von ein Hardware in einer Datumsbereich 
    Task<bool> CheckAvailability(List<Guid> hardwareIds, List<int> quantities, DateTime startDate, DateTime endDate, Guid eventId);
    //holt alle Reservierungen
    Task<IEnumerable<Reservation>> GetAllReservations();

  }
}
