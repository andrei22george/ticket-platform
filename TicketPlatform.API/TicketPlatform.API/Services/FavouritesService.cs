using AutoMapper;
using ErrorOr;
using TicketPlatform.API.Model.In;
using TicketPlatform.API.Model;
using TicketPlatform.API.Repositories.Interfaces;

namespace TicketPlatform.API.Services
{
    public class FavouritesService
    {
        private IFavouritesRepository _repository;
        private readonly IMapper _mapper;

        public FavouritesService(IFavouritesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<Favourites> GetAllFavouritess(QueryParameters parameters)
        {
            return _repository.GetAllFavourites(parameters);
        }

        public ErrorOr<Favourites> GetFavouritesById(int id)
        {
            return _repository.GetFavouritesById(id);
        }

        public int InsertFavourites(FavouritesIn eventIn)
        {
            var favourites = _mapper.Map<Favourites>(eventIn);

            return _repository.InsertFavourites(favourites);
        }

        public bool UpsertFavourites(int id, FavouritesIn eventIn)
        {
            var favourites = _mapper.Map<Favourites>(eventIn);

            return _repository.UpsertFavourites(id, favourites);
        }

        public bool DeleteFavourites(int id)
        {
            return _repository.DeleteFavourites(id);
        }

        public bool DeleteFavouritess(List<int> ids)
        {
            return _repository.DeleteFavourites(ids);
        }
    }
}
