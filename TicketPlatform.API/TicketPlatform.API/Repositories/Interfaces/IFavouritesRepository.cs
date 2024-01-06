using ErrorOr;
using TicketPlatform.API.Model;

namespace TicketPlatform.API.Repositories.Interfaces
{
    public interface IFavouritesRepository
    {
        public List<Favourites> GetAllFavourites(QueryParameters parameters);
        public ErrorOr<Favourites> GetFavouritesById(int id);
        public int InsertFavourites(Favourites favourites);
        public bool UpsertFavourites(int id, Favourites favourites);
        public bool DeleteFavourites(int id);
        public bool DeleteFavourites(List<int> ids);
    }
}