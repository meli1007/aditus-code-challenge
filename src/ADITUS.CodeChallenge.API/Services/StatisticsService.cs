using ADITUS.CodeChallenge.API.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ADITUS.CodeChallenge.API.Services
{
  public class StatisticsService : IStatisticsServices
  {
    private static object client;

    public Task<Statistics> GetStatistic(Guid id)
    {
      throw new NotImplementedException();
    }

    public Task<IList<Statistics>> GetStatistics()
    {
      throw new NotImplementedException();
    }

  }
}
