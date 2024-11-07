

using ADITUS.CodeChallenge.API.Services;

namespace ADITUS.CodeChallenge.API.Domain
{
  public record EventHybrid
  {
    public Guid Id { get; set; }
    public EventType Type { get; set; }
    public int Attendees { get; set; }
    public int Invites { get; set; }
    public int Visits { get; set; }
    public int VirtualRooms { get; set; }
    public int VisitorsCount { get; set; }
    public int ExhibitorsCount { get; set; }
    public int BoothsCount { get; set; }
  }
}
