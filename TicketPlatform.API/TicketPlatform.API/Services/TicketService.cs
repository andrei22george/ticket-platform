using AutoMapper;
using ErrorOr;
using TicketPlatform.API.Model.In;
using TicketPlatform.API.Model;
using TicketPlatform.API.Repositories.Interfaces;

namespace TicketPlatform.API.Services
{
    public class TicketService
    {
        private ITicketRepository _repository;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<Ticket> GetAllTickets(QueryParameters parameters)
        {
            return _repository.GetAllTickets(parameters);
        }

        public ErrorOr<Ticket> GetTicketById(int id)
        {
            return _repository.GetTicketById(id);
        }

        public int InsertTicket(TicketIn ticketIn)
        {
            var ticket = _mapper.Map<Ticket>(ticketIn);

            return _repository.InsertTicket(ticket);
        }

        public bool UpsertTicket(int id, TicketIn ticketIn)
        {
            var ticket = _mapper.Map<Ticket>(ticketIn);

            return _repository.UpsertTicket(id, ticket);
        }

        public bool DeleteTicket(int id)
        {
            return _repository.DeleteTicket(id);
        }

        public bool DeleteTickets(List<int> ids)
        {
            return _repository.DeleteTickets(ids);
        }
    }
}
