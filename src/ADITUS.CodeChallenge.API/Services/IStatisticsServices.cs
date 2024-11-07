using ADITUS.CodeChallenge.API.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ADITUS.CodeChallenge.API.Services
{
  public interface IStatisticsServices
  {
    Task<Statistics> GetStatistic(Guid id);
    Task<IList<Statistics>> GetStatistics();
  }
}
