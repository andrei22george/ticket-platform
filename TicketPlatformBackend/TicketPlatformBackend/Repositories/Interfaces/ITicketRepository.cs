using ErrorOr;
using TicketPlatformBackend.Model;

namespace TicketPlatformBackend.Repositories.Interfaces
{
    internal interface ITicketRepository
    {
        public List<Ticket> GetAllTickets(QueryParameters parameters);
        public ErrorOr<Ticket> GetTicketById(int id);
        public int InsertTicket(Ticket ticket);
        public bool UpsertTicket(int id, Ticket ticket);
        public bool DeleteTicket(int id);
        public bool DeleteTickets(List<int> ids);
    }
}
