namespace ADITUS.CodeChallenge.API.Domain
{
  public record InMemoryStorage
  {
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
  }
}
