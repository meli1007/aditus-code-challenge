namespace ADITUS.CodeChallenge.API.Domain
{
  public class ReservationRequest
  {
    public Guid ReservationId { get; set; }
    public List<string> HardwareName { get; set; }
    public List<Guid> HardwareIds { get; set; }
    public List<int> Quantities { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string EventName { get; set; }
    public List<int> ReservedQuantities { get; set; }
  }
}
