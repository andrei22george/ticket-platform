using AutoMapper;
using ErrorOr;
using TicketPlatform.API.Model.In;
using TicketPlatform.API.Model;
using TicketPlatform.API.Repositories.Interfaces;

namespace TicketPlatform.API.Services
{
    public class CartService
    {
        private ICartRepository _repository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<Cart> GetAllCarts(QueryParameters parameters)
        {
            return _repository.GetAllCarts(parameters);
        }

        public ErrorOr<Cart> GetCartById(int id)
        {
            return _repository.GetCartByUserId(id);
        }

        public int InsertCart(CartIn eventIn)
        {
            var cart = _mapper.Map<Cart>(eventIn);

            return _repository.InsertCart(cart);
        }

        public bool UpsertCart(int id, CartIn eventIn)
        {
            var cart = _mapper.Map<Cart>(eventIn);

            return _repository.UpsertCart(id, cart);
        }

        public bool DeleteCart(int id)
        {
            return _repository.DeleteCart(id);
        }

        public bool DeleteCarts(List<int> ids)
        {
            return _repository.DeleteCarts(ids);
        }
    }
}
