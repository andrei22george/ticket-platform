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

        public int InsertCart(CartIn cartIn)
        {
            var cart = _mapper.Map<Cart>(cartIn);

            return _repository.InsertCart(cart);
        }

        public int UpsertCart(CartIn cartIn)
        {
            var cart = _mapper.Map<Cart>(cartIn);

            return _repository.UpsertCart(cart);
        }

        public bool DeleteCart(int idUser, int idEvent)
        {
            return _repository.DeleteCart(idUser, idEvent);
        }

        public bool DeleteCarts(List<int> ids)
        {
            return _repository.DeleteCarts(ids);
        }

        public void SendEmail(string email)
        {
            _repository.SendEmail(email); 
        }
    }
}
