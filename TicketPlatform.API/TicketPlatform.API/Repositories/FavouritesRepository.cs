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
    public class FavouritesRepository : IFavouritesRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly Configurations _configuration;

        public FavouritesRepository(IOptions<Configurations> configurations)
        {
            _configuration = configurations.Value;
            _sqlConnection = new SqlConnection(_configuration.ConnectionString);
        }

        public List<Favourites> GetAllFavourites(QueryParameters parameters)
        {
            return _sqlConnection.GetAll<Favourites>().ToList();
        }

        public ErrorOr<Favourites> GetFavouritesById(int id)
        {
            var favourites = _sqlConnection.Get<Favourites>(id);

            return favourites == null ? Errors.Admin.NotFound : favourites;
        }

        public int InsertFavourites(Favourites favourites)
        {
            return (int)_sqlConnection.Insert(favourites);
        }

        public bool UpsertFavourites(int id, Favourites favourites)
        {
            favourites.Id = id;

            return _sqlConnection.Update(favourites);
        }

        public bool DeleteFavourites(int id)
        {
            return _sqlConnection.Delete(new Favourites { Id = id });
        }

        public bool DeleteFavourites(List<int> ids)
        {
            var favouritesToDelete = ids.Select(id => new Favourites { Id = id }).ToList();

            return _sqlConnection.Delete(favouritesToDelete);
        }
    }
}
