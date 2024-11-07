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
    public Task<bool> CheckAvailability(List<Guid> hardwareIds, List<int> quantities, DateTime startDate, DateTime endDate, Guid eventId)
    {
      //Reservierung soll mind. 4 Wochen vor der StarDate von der Event passieren
      if ((startDate - DateTime.Now).TotalDays < 28)
      {
        return Task.FromResult(false);
      }
      var reservations = _cache.Get<List<Reservation>>(CacheKey);

      foreach (var hardwareId in hardwareIds)
      {
        // prüf, ob eine Reservierung für den Komponenten vorhanden ist
        var hasConflictingReservation = reservations.Any(reservation =>
          reservation.EventId != eventId && //verhindert, dass zwei verschiedene Events mit demselben Datum dieselbe Hardware reservieren
          reservation.HardwareIds.Contains(hardwareId) &&
          (startDate == reservation.EndDate && endDate == reservation.StartDate)); // Konflikte bei gleichen Datum 

        if (hasConflictingReservation)
        {
          return Task.FromResult(false);
        }
      }
      return Task.FromResult(true); //Reservierung hat geklappt
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
      // Prüft, ob bereits eine Reservierung für dasselbe Event und dieselbe Hardware besteht
      var hasExistingReservation = reservations.Any(r =>
        r.EventId == eventId &&
        r.HardwareIds.Intersect(hardwareIds).Any()); // verhindert eine Überschneidung von Veranstaltung und Reservierung
      if (hasExistingReservation)
      {
        // wenn Reservierung vorhanden ist, dann wird keiner neu gemacht
        return false;
      }

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
        var quantityToReserve = quantities[i]; // wie viel man reservieren möchte

        var hardware = await _hardwareService.GetComponent(hardwareId);

        hardwareNames.Add(hardware.Name);
        reservedQuantities.Add(quantityToReserve); // reservierte Menge für diesen Event speichern
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