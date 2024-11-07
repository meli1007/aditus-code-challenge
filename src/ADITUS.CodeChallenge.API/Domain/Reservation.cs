namespace ADITUS.CodeChallenge.API.Domain
{
  public class Reservation
  {
    public Guid Id { get; set; } = Guid.NewGuid(); // neue Guid, wenn eine Reservierung gemacht wird
    public Guid EventId { get; set; } 
    public string EventName { get; set; }
    public List<Guid> HardwareIds { get; set; } 
    public List<string> HardwareNames { get; set; }
    public List<int> Quantities { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Confirmed { get; set; }
    // Fügt eine Liste der reservierten Mengen pro Event für jede Hardwarekomponente hinzu
    public List<int> ReservedQuantities { get; set; }
  }
}
