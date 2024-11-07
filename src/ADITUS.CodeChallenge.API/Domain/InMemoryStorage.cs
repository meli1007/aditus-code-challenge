namespace ADITUS.CodeChallenge.API.Domain
{
  public class InMemoryStorage
  {
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
  }
}
