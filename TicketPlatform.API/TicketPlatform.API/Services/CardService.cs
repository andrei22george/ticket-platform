using AutoMapper;
using ErrorOr;
using TicketPlatform.API.Model.In;
using TicketPlatform.API.Model;
using TicketPlatform.API.Repositories.Interfaces;

namespace TicketPlatform.API.Services
{
    public class CardService
    {
        private ICardRepository _repository;
        private readonly IMapper _mapper;

        public CardService(ICardRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<Card> GetAllCards(QueryParameters parameters)
        {
            return _repository.GetAllCards(parameters);
        }

        public ErrorOr<Card> GetCardById(int id)
        {
            return _repository.GetCardById(id);
        }

        public int InsertCard(CardIn eventIn)
        {
            var card = _mapper.Map<Card>(eventIn);

            return _repository.InsertCard(card);
        }

        public bool UpsertCard(int id, CardIn eventIn)
        {
            var card = _mapper.Map<Card>(eventIn);

            return _repository.UpsertCard(id, card);
        }

        public bool DeleteCard(int id)
        {
            return _repository.DeleteCard(id);
        }

        public bool DeleteCards(List<int> ids)
        {
            return _repository.DeleteCards(ids);
        }
    }
}
