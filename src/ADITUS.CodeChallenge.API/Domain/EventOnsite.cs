using ADITUS.CodeChallenge.API.Domain;

namespace ADITUS.CodeChallenge.API.Services
{
  public record EventOnsite
  {
    public Guid Id { get; set; }
    public EventType Type { get; set; }
    public int VisitorsCount { get; set; }
    public int ExhibitorsCount { get; set; }
    public int BoothsCount { get; set; }
  }
}
