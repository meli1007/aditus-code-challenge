namespace ADITUS.CodeChallenge.API.Domain
{
  public record Event
  {
    public Guid Id { get; init; } 
    public int Year { get; init; }
    public string Name { get; init; }
    public EventType Type { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
  }
}