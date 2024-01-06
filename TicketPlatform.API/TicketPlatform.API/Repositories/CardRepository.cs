using Dapper.Contrib.Extensions;
using ErrorOr;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using TicketPlatform.API.Model;
using TicketPlatform.API.Repositories.Interfaces;
using TicketPlatform.API.ServiceErrors;
using TicketPlatform.API.Services;

namespace TicketPlatform.API.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly Configurations _configuration;

        public CardRepository(IOptions<Configurations> configurations)
        {
            _configuration = configurations.Value;
            _sqlConnection = new SqlConnection(_configuration.ConnectionString);
        }

        public List<Card> GetAllCards(QueryParameters parameters)
        {
            return _sqlConnection.GetAll<Card>().ToList();
        }

        public ErrorOr<Card> GetCardById(int id)
        {
            var card = _sqlConnection.Get<Card>(id);

            return card == null ? Errors.Card.NotFound : card;
        }

        public int InsertCard(Card card)
        {
            return (int)_sqlConnection.Insert(card);
        }

        public bool UpsertCard(int id, Card card)
        {
            card.Id = id;

            return _sqlConnection.Update(card);
        }

        public bool DeleteCard(int id)
        {
            return _sqlConnection.Delete(new Card { Id = id });
        }

        public bool DeleteCards(List<int> ids)
        {
            var cardsToDelete = ids.Select(id => new Card { Id = id }).ToList();

            return _sqlConnection.Delete(cardsToDelete);
        }
    }
}
