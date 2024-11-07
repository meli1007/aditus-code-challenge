using ADITUS.CodeChallenge.API.Domain;

namespace ADITUS.CodeChallenge.API.Services
{
  public interface IHardwareService
  {
    //Holt ein Hardware-Component je nach HardwareId
    Task<Hardware> GetComponent(Guid componentId);
    //Holt alle Hardware-Components
    Task<IList<Hardware>> GetComponents();

  }
}
