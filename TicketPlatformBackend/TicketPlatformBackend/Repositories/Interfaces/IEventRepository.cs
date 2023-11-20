using ErrorOr;
using TicketPlatformBackend.Model;

namespace TicketPlatformBackend.Repositories.Interfaces
{
    public interface IEventRepository
    {
        public List<Event> GetAllEvents(QueryParameters parameters);
        public ErrorOr<Event> GetEventById(int id);
        public int InsertEvent(Event ev);
        public bool UpsertEvent(int id, Event ev);
        public bool DeleteEvent(int id);
        public bool DeleteEvents(List<int> ids);
    }
}
