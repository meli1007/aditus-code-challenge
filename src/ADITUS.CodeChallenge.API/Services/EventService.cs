using System.Net.Http;
using ADITUS.CodeChallenge.API.Services;
using ADITUS.CodeChallenge.API.Domain;
using Newtonsoft.Json;

namespace ADITUS.CodeChallenge.API.Services
{
  public class EventService : IEventService
  {
    private readonly IList<Event> _events;
    private readonly HttpClient _httpClient;
    public EventService(HttpClient httpClient)
    {
      _httpClient = httpClient;
      _events = new List<Event>
      {
        new Event
        {
          Id = Guid.Parse("7c63631c-18d4-4395-9c1e-886554265eb0"),
          Year = 2019,
          Name = "ADITUS Code Challenge 2019",
          StartDate = new DateTime(2019, 1, 1),
          EndDate = new DateTime(2019, 1, 31),
          Type = EventType.OnSite
        },
        new Event
        {
          Id = Guid.Parse("751fd775-2c8e-48e0-955c-2144008e984a"),
          Year = 2020,
          Name = "ADITUS Code Challenge 2020",
          StartDate = new DateTime(2020, 1, 1),
          EndDate = new DateTime(2020, 1, 15),
          Type = EventType.Hybrid
        },
        new Event
        {
          Id = Guid.Parse("974098e0-9b3f-41d5-80c2-551600ad204a"),
          Year = 2021,
          Name = "ADITUS Code Challenge 2021",
          StartDate = new DateTime(2021, 1, 1),
          EndDate = new DateTime(2021, 1, 18),
          Type = EventType.Online
        },
        new Event
        {
          Id = Guid.Parse("28669572-2b9a-4b2c-ad7e-6434ea8ab761"),
          Year = 2022,
          Name = "ADITUS Code Challenge 2022",
          StartDate = new DateTime(2022, 1, 1),
          EndDate = new DateTime(2022, 1, 11),
          Type = EventType.Online
        },
        new Event
        {
          Id = Guid.Parse("3a17b294-8716-448c-94db-ebf9bf53f1ce"),
          Year = 2023,
          Name = "ADITUS Code Challenge 2023",
          StartDate = new DateTime(2023, 1, 1),
          EndDate = new DateTime(2023, 1, 23),
          Type = EventType.OnSite
        }
      };
    }

    public Task<Event> GetEvent(Guid id)
    {
      var @event = _events.FirstOrDefault(e => e.Id == id);
      return Task.FromResult(@event);
    }

    public Task<IList<Event>> GetEvents()
    {
      return Task.FromResult(_events);
    }
    public async Task<(List<EventOnsite>, List<EventOnline>, List<EventHybrid>)> GetEventsWithStatistics()
    {
      
      var eventOnsite = new List<EventOnsite>();
      var eventOnline = new List<EventOnline>();
      var eventHybrid = new List<EventHybrid>();

      foreach (var myEvent in _events)
      {
        List<string> apiUrls = new List<string>();
        HttpResponseMessage response;

        switch (myEvent.Type)
        {
          case EventType.OnSite:
            apiUrls.Add($"https://codechallenge-statistics.azurewebsites.net/api/onsite-statistics/{myEvent.Id}");
            break;

          case EventType.Online:
            apiUrls.Add($"https://codechallenge-statistics.azurewebsites.net/api/online-statistics/{myEvent.Id}");
            break;

          case EventType.Hybrid:
            apiUrls.Add($"https://codechallenge-statistics.azurewebsites.net/api/onsite-statistics/{myEvent.Id}");
            apiUrls.Add($"https://codechallenge-statistics.azurewebsites.net/api/online-statistics/{myEvent.Id}");
            break;
        }

        EventOnsite onsiteStatistic = null;
        EventOnline onlineStatistic = null;

        foreach (var apiUrl in apiUrls)
        {
          response = await _httpClient.GetAsync(apiUrl);
          if (response.IsSuccessStatusCode)
          {
            var jsonData = await response.Content.ReadAsStringAsync();

            if (apiUrl.Contains("onsite-statistics"))
            {
              onsiteStatistic = JsonConvert.DeserializeObject<EventOnsite>(jsonData);
              onsiteStatistic.Id = myEvent.Id;
              onsiteStatistic.Type = myEvent.Type;
              eventOnsite.Add(onsiteStatistic);
            }
            else if (apiUrl.Contains("online-statistics"))
            {
              onlineStatistic = JsonConvert.DeserializeObject<EventOnline>(jsonData);
              onlineStatistic.Id = myEvent.Id;
              onlineStatistic.Type = myEvent.Type;
              eventOnline.Add(onlineStatistic);
            }
          }
          else
          {
            throw new HttpRequestException("Error al obtener datos de la API.");
          }
        }

        if (myEvent.Type == EventType.Hybrid && onsiteStatistic != null && onlineStatistic != null)
        {
          var combinedStatistics = new EventHybrid
          {
            Id = myEvent.Id,
            Type = myEvent.Type,
            Attendees = onlineStatistic.Attendees,
            Invites = onlineStatistic.Invites,
            Visits = onlineStatistic.Visits,
            VirtualRooms = onlineStatistic.VirtualRooms,
            VisitorsCount = onsiteStatistic.VisitorsCount,
            ExhibitorsCount = onsiteStatistic.ExhibitorsCount,
            BoothsCount = onsiteStatistic.BoothsCount
          };
          eventHybrid.Add(combinedStatistics);
        }
      }

      return (eventOnsite, eventOnline, eventHybrid);
    }

  }
}