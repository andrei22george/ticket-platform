using ErrorOr;
using TicketPlatform.API.Model;

namespace TicketPlatform.API.Repositories.Interfaces
{
    public interface ICardRepository
    {
        public List<Card> GetAllCards(QueryParameters parameters);
        public ErrorOr<Card> GetCardById(int id);
        public int InsertCard(Card card);
        public bool UpsertCard(int id, Card card);
        public bool DeleteCard(int id);
        public bool DeleteCards(List<int> ids);
    }
}