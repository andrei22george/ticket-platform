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
    public class CartRepository : ICartRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly Configurations _configuration;

        public CartRepository(IOptions<Configurations> configurations)
        {
            _configuration = configurations.Value;
            _sqlConnection = new SqlConnection(_configuration.ConnectionString);
        }

        public List<Cart> GetAllCarts(QueryParameters parameters)
        {
            return _sqlConnection.GetAll<Cart>().ToList();
        }

        public ErrorOr<Cart> GetCartByUserId(int idUser)
        {
            var card = _sqlConnection.Get<Cart>(idUser);

            return card;
        }

        public int InsertCart(Cart cart)
        {
            return (int)_sqlConnection.Insert(cart);
        }

        public bool UpsertCart(int idUser, Cart cart)
        {
            cart.IdUser = idUser;

            return _sqlConnection.Update(cart);
        }

        public bool DeleteCart(int idUser)
        {
            return _sqlConnection.Delete(new Cart { IdUser = idUser });
        }

        public bool DeleteCarts(List<int> ids)
        {
            var cartsToDelete = ids.Select(id => new Cart { IdUser = id }).ToList();

            return _sqlConnection.Delete(cartsToDelete);
        }
    }
}
