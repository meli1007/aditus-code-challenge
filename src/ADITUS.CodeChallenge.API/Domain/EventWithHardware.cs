namespace ADITUS.CodeChallenge.API.Domain
{
  public record EventWithHardware
  {
    public Guid Id { get; set; }  
    public string Name { get; set; } 
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; }
    //Liste der reservierten Komponenten-IDs; reservierte Komponenten speichern
    public List<Hardware> ReservedComponents { get; set; } = new List<Hardware>(); 
  }
}
