using ADITUS.CodeChallenge.API.Domain;

namespace ADITUS.CodeChallenge.API.Services
{
    public class HardwareService : IHardwareService
    {
      private readonly List<Hardware> _components;

      public HardwareService()
      {
      _components = new List<Hardware>
        {
          new Hardware
          {
              Id = Guid.Parse("c5dcc085-118e-415e-b8ba-2c08721f49ad"),
              Name = "Drehsperre",
              Amount = 5,
              Status = true
          },
          new Hardware
          {
              Id = Guid.Parse("965d9c34-b183-445e-a75a-afdf61d3b07c"),
              Name = "Funkhandscanner",
              Amount = 10,
              Status = true
          },
          new Hardware
          {
              Id = Guid.Parse("abd32292-645f-49c6-9284-0de57afd6a44"),
              Name = "Mobiles Scan-Terminal",
              Amount = 20,
              Status = true
          }
        };
      }
      public Task<Hardware> GetComponent(Guid id)
      {
        var @component = _components.FirstOrDefault(e => e.Id == id);
        return Task.FromResult(@component);
      }

      public Task<IList<Hardware>> GetComponents()
      {
        return Task.FromResult<IList<Hardware>>(_components);
      }
    }
}
