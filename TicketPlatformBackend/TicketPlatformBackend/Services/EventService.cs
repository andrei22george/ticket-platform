using AutoMapper;
using ErrorOr;
using TicketPlatformBackend.Model.In;
using TicketPlatformBackend.Model;
using TicketPlatformBackend.Repositories.Interfaces;

namespace TicketPlatformBackend.Services
{
    internal class EventService
    {
        private IEventRepository _repository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<Event> GetAllEvents(QueryParameters parameters)
        {
            return _repository.GetAllEvents(parameters);
        }

        public ErrorOr<Event> GetEventById(int id)
        {
            return _repository.GetEventById(id);
        }

        public int InsertEvent(EventIn eventIn)
        {
            var ev = _mapper.Map<Event>(eventIn);

            return _repository.InsertEvent(ev);
        }

        public bool UpsertEvent(int id, EventIn eventIn)
        {
            var ev = _mapper.Map<Event>(eventIn);

            return _repository.UpsertEvent(id, ev);
        }

        public bool DeleteEvent(int id)
        {
            return _repository.DeleteEvent(id);
        }

        public bool DeleteEvents(List<int> ids)
        {
            return _repository.DeleteEvents(ids);
        }
    }
}
