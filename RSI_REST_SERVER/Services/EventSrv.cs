using RSI_REST_SERVER.Models;
using System.Diagnostics.Tracing;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RSI_REST_SERVER.Services
{
    public interface IEventSrv
    {
        List<Event> GetEventsForDay(DateTime date);
        List<Event> GetEventsForWeek(DateTime date);
        Event GetEventInformation(int eventId);
        int AddEvent(string name, string type, DateTime date, string description);
        void ModifyEventInformation(int eventId, string name, string type, DateTime date, string description);
        List<Event> GetAllEvents(HttpRequest request);
        void RemoveEvent(int id);

    }
    public class EventSrv : IEventSrv
    {
        public List<Event> events = new List<Event>();
        public EventSrv()
        {
            events.Add(new Event(1, "Dni Białegostoku", "Festiwal", new DateTime(2024, 5, 5), "Coroczne święto, podczas którego prezentowane są kultura, jedzenie i tradycje Białegostoku."));
            events.Add(new Event(2, "Półmaraton Białegostoku", "Sport", new DateTime(2024, 5, 5), "Popularne wydarzenie biegowe, przyciągające zawodników z całego kraju."));
            events.Add(new Event(3, "Festiwal Jazzowy w Białymstoku", "Muzyka", new DateTime(2024, 5, 6), "Trzy dni jazzowych występów lokalnych i międzynarodowych artystów."));
            events.Add(new Event(4, "Festiwal Filmowy w Białymstoku", "Sztuka", new DateTime(2024, 5, 6), "Święto niezależnego kina z pokazami, warsztatami i sesjami Q&A."));
            events.Add(new Event(5, "Jarmark Bożonarodzeniowy w Białymstoku", "Święta", new DateTime(2024, 5, 10), "Świąteczny jarmark oferujący rękodzieło, tradycyjne smakołyki i rozrywkę."));
            events.Add(new Event(6, "Spacer Artystyczny po Białymstoku", "Sztuka", new DateTime(2024, 5, 10), "Prowadzone przez lokalnych artystów spacery po vibrantnej scenie street artu w Białymstoku."));
            events.Add(new Event(7, "Festiwal Piwa w Białymstoku", "Jedzenie & Napoje", new DateTime(2024, 5, 14), "Weekendowe święto piwa rzemieślniczego z degustacjami, muzyką na żywo i aktywnościami związanymi z piwem."));
            events.Add(new Event(8, "Tydzień Teatralny w Białymstoku", "Sztuka", new DateTime(2024, 5, 14), "Prezentacja przedstawień teatralnych lokalnych grup dramatycznych oraz gości."));
            events.Add(new Event(9, "Targi Książki w Białymstoku", "Literatura", new DateTime(2024, 5, 14), "Wydarzenie dla miłośników książek z czytaniem przez autorów, podpisywaniem książek i dyskusjami literackimi."));
            events.Add(new Event(10, "Festiwal Wiosenny w Białymstoku", "Festiwal", new DateTime(2024, 5, 14), "Święto wiosny z koncertami plenerowymi, wystawami sztuki i atrakcjami dla całej rodziny."));
        }

        public List<Event> GetEventsForDay(DateTime date)
        {
            return events.Where(p => p.Details.Date.Date == date.Date.AddDays(1)).ToList();
        }
        public List<Event> GetEventsForWeek(DateTime date)
        {
            return events.Where(p => Utilities.Utilities.FirstDayOfWeek(p.Details.Date.Date) <= date.Date.AddDays(1)
            && Utilities.Utilities.LastDayOfWeek(p.Details.Date.Date) >= date.Date.AddDays(1)).ToList();
        }

        public Event GetEventInformation(int eventId)
        {
            var res = events.First(p => p.Id == eventId);
            return res;
        }
        public int AddEvent(string name, string type, DateTime date, string description)
        {
            var id = events.OrderByDescending(p => p.Id).First().Id + 1;
            events.Add(new Event(id, name, type, date, description));
            return id;
        }

        public void RemoveEvent(int id)
        {
            events.Remove(events.FirstOrDefault(p=>p.Id == id));
        }

        public void ModifyEventInformation(int eventId, string name, string type, DateTime date, string description)
        {
            var eventToChange = events.FirstOrDefault(p => p.Id == eventId);

            if (eventToChange is null)
                throw new Exception("Event doesn't exist");

            eventToChange.Name = name;
            eventToChange.Details.Type = type;
            eventToChange.Details.Date = date;
            eventToChange.Details.Description = description;
        }
        public List<Event> GetAllEvents(HttpRequest request)
        {
            var eventList = new List<Event>();
            var url = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetEncodedUrl(request);
            foreach (var ev in events)
            {
                ev.Links = new List<Link>()
                {
                    new Link($"{url}/{ev.Id}", "self"),
                    new Link($"{url}/{ev.Id}/Information", "Information")
                };
                eventList.Add(ev);
            }

            return eventList;

        }
    }
}
