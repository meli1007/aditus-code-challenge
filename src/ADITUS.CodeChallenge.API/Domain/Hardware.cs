namespace ADITUS.CodeChallenge.API.Domain
{
  public record Hardware
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public bool Status { get; set; }

  }
}
