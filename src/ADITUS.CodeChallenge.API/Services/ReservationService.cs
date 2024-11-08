using System;
using ADITUS.CodeChallenge.API.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace ADITUS.CodeChallenge.API.Services
{
  public class ReservationService : IReservationService
  {
    private readonly IMemoryCache _cache;
    private readonly IHardwareService _hardwareService; 
    private const string CacheKey = "Reservations"; // Schlüssel zum Speichern der Reservierungsliste

    public ReservationService(IMemoryCache cache, IHardwareService hardwareService)
    {
      _cache = cache;
      _hardwareService = hardwareService;
      //Initialisierung von einer Reservierungsliste in Cache
      if (!_cache.TryGetValue(CacheKey, out List<Reservation> _))
      {
        _cache.Set(CacheKey, new List<Reservation>());
      }
    }
    public async Task<bool> CheckAvailability(List<Guid> hardwareIds, List<int> quantities, DateTime startDate, DateTime endDate, Guid eventId)
    {
      //Reservierung soll mind. 4 Wochen vor der StarDate von der Event passieren
      if ((startDate - DateTime.Now).TotalDays < 28)
      {
        return false;
      }
      var reservations = _cache.Get<List<Reservation>>(CacheKey);

      // prüft, ob es für ein Event schon einer Reservierung gibt
      var existingReservation = reservations.FirstOrDefault(r => r.EventId == eventId);
      if (existingReservation != null)
      {
        return false; 
      }
      var reservedQuantities = new Dictionary<Guid, int>();

      for (var i = 0; i < hardwareIds.Count; i++)
      {
        var currentHardwareId = hardwareIds[i];
        var quantityToReserve = quantities[i];

        // aktuelle info von der Hardware
        var hardware = await _hardwareService.GetComponent(currentHardwareId);
        if (hardware == null)
        {
          return false; // Hardware nicht gefunden
        }

        // kalkuliert den Gesamtbetrag der bestehenden Reservierungen für die Hardware zu diesen Terminen
        var totalReservedCount = reservations
            .Where(reservation => reservation.HardwareIds.Contains(currentHardwareId) &&
                                  reservation.StartDate == startDate &&
                                  reservation.EndDate == endDate)
            .Sum(reservation => reservation.Quantities[reservation.HardwareIds.IndexOf(currentHardwareId)]);

        // prüft die Verfügbarkeit unter Berücksichtigung der reservierten Gesamtmenge und der neuen angeforderten Menge
        if (totalReservedCount + quantityToReserve > hardware.Amount)
        {
          return false; // Es gibt kein genüg Menge für dieser Hardware
        }
      }

      return true; //Reservierung hat geklappt
    }

    public async Task<IEnumerable<Reservation>> GetAllReservations()
    {
      var reservations = _cache.Get<List<Reservation>>(CacheKey);
      return await Task.FromResult(reservations);
    }

    public async Task<IEnumerable<Reservation>> GetReservationsForComponentAsync(Guid hardwareId)
    {
      var reservations = _cache.Get<List<Reservation>>(CacheKey);
      var componentReservations = reservations.Where(r => r.HardwareIds.Contains(hardwareId));
      return await Task.FromResult(componentReservations);
    }

    public async Task<bool> ReserveHardwareComponent(Guid eventId, string eventName, List<Guid> hardwareIds, List<int> quantities, DateTime startDate, DateTime endDate)
    {
      // holt alle Liste von Reservierung aus dem Cache
      var reservations = _cache.Get<List<Reservation>>(CacheKey);

      // Verfügbarkeit von Hardware vor dem Reservierung weiter püfen
      if (!await CheckAvailability(hardwareIds, quantities, startDate, endDate, eventId))
      {
        return false;
      }

      var hardwareNames = new List<string>();
      var reservedQuantities = new List<int>(); // Zum Speichern reservierter Mengen pro Event
      
      // Menge für die Reservierung
      for (var i = 0; i < hardwareIds.Count; i++)
      {
        var hardwareId = hardwareIds[i];
        var quantityToReserve = quantities[i];
         // wie viel man reservieren möchte

        var hardware = await _hardwareService.GetComponent(hardwareId);
        hardwareNames.Add(hardware.Name);
        reservedQuantities.Add(quantityToReserve);
         // reservierte Menge für diesen Event speichern
      }
     
      //neue Reservierung hinzufügen
      var newReservation = new Reservation
      {
        Id = Guid.NewGuid(), 
        EventId = eventId,
        EventName = eventName,
        HardwareIds = hardwareIds, 
        HardwareNames = hardwareNames,
        Quantities = quantities,
        StartDate = startDate,
        EndDate = endDate,
        ReservedQuantities = reservedQuantities, // fügt reservierte Mengen pro Veranstaltung hinzu
        Confirmed = false 
      };

      reservations.Add(newReservation);
      _cache.Set(CacheKey, reservations); // Cache aktualisieren
      
      return true;
    }
  }
}