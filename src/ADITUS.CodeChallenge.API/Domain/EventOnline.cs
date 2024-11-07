using ADITUS.CodeChallenge.API.Domain;

namespace ADITUS.CodeChallenge.API.Services
{
  public record EventOnline
  {
    public Guid Id { get; set; }
    public EventType Type { get; set; }
    public int Attendees { get; set; }
    public int Invites { get; set; }
    public int Visits { get; set; }
    public int VirtualRooms { get; set; }
  }
}
