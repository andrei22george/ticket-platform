using Dapper.Contrib.Extensions;
using ErrorOr;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using TicketPlatform.API.Model;
using TicketPlatform.API.Repositories.Interfaces;
using TicketPlatform.API.Services;

namespace TicketPlatform.API.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly Configurations _configuration;

        public EventRepository(IOptions<Configurations> configurations)
        {
            _configuration = configurations.Value;
            _sqlConnection = new SqlConnection(_configuration.ConnectionString);
        }

        public List<Event> GetAllEvents(QueryParameters parameters)
        {
            return _sqlConnection.GetAll<Event>().ToList();
        }

        public ErrorOr<Event> GetEventById(int id)
        {
            var ev = _sqlConnection.Get<Event>(id);

            return ev; //== null ? Errors.Director.NotFound : director;
        }

        public int InsertEvent(Event ev)
        {
            return (int)_sqlConnection.Insert(ev);
        }

        public bool UpsertEvent(int id, Event ev)
        {
            ev.Id = id;

            return _sqlConnection.Update(ev);
        }

        public bool DeleteEvent(int id)
        {
            return _sqlConnection.Delete(new Event { Id = id });
        }

        public bool DeleteEvents(List<int> ids)
        {
            var eventsToDelete = ids.Select(id => new Event { Id = id }).ToList();

            return _sqlConnection.Delete(eventsToDelete);
        }
    }
}
