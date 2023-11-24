using ErrorOr;
using TicketPlatform.API.Model;

namespace TicketPlatform.API.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        public List<Ticket> GetAllTickets(QueryParameters parameters);
        public ErrorOr<Ticket> GetTicketById(int id);
        public int InsertTicket(Ticket ticket);
        public bool UpsertTicket(int id, Ticket ticket);
        public bool DeleteTicket(int id);
        public bool DeleteTickets(List<int> ids);
    }
}
