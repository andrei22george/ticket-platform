using Dapper.Contrib.Extensions;
using ErrorOr;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using TicketPlatform.API.Model;
using TicketPlatform.API.Repositories.Interfaces;
using TicketPlatform.API.Services;

namespace TicketPlatform.API.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly Configurations _configuration;

        public TicketRepository(IOptions<Configurations> configurations)
        {
            _configuration = configurations.Value;
            _sqlConnection = new SqlConnection(_configuration.ConnectionString);
        }

        public List<Ticket> GetAllTickets(QueryParameters parameters)
        {
            return _sqlConnection.GetAll<Ticket>().ToList();
        }

        public ErrorOr<Ticket> GetTicketById(int id)
        {
            var ticket = _sqlConnection.Get<Ticket>(id);

            return ticket; //== null ? Errors.Director.NotFound : director;
        }

        public int InsertTicket(Ticket ticket)
        {
            return (int)_sqlConnection.Insert(ticket);
        }

        public bool UpsertTicket(int id, Ticket ticket)
        {
            ticket.Id = id;

            return _sqlConnection.Update(ticket);
        }

        public bool DeleteTicket(int id)
        {
            return _sqlConnection.Delete(new Ticket { Id = id });
        }

        public bool DeleteTickets(List<int> ids)
        {
            var ticketsToDelete = ids.Select(id => new Ticket { Id = id }).ToList();

            return _sqlConnection.Delete(ticketsToDelete);
        }
    }
}
