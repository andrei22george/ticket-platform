using AutoMapper;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TicketPlatform.API.Model.In;
using TicketPlatform.API.Model;
using TicketPlatform.API.Services;
using TicketPlatform.API.Utilities;
using FluentValidation.Results;
using TicketPlatform.API.ServiceErrors;

namespace TicketPlatform.API.Controllers
{
    [ApiController]
    [Route("favourites")]
    public class FavouritesController : ControllerBase
    {
        private readonly FavouritesService favouritesService;
        private readonly ILogger<FavouritesController> _logger;

        public FavouritesController(ILogger<FavouritesController> logger, FavouritesService service, IMapper mapper)
        {
            _logger = logger;
            favouritesService = service;
        }

        [HttpGet]
        public IEnumerable<Favourites> Get([FromQuery] QueryParameters parameters)
        {
            return favouritesService.GetAllFavouritess(parameters);
        }

        [HttpGet("{id}")]
        public ErrorOr<Favourites> GetFavouritesById(int id)
        {
            return favouritesService.GetFavouritesById(id);
        }

        [HttpPost]
        public ErrorOr<int> CreateFavourites([FromBody] FavouritesIn request)
        {
            var validator = new FavouritesInValidator();
            ValidationResult results = validator.Validate(request);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return Errors.Favourites.FailedValidation;
            }
            
            return favouritesService.InsertFavourites(request);
        }

        [HttpPut("{id}")]
        public ErrorOr<bool> UpsertFavourites(int id, [FromBody] FavouritesIn request)
        {
            var validator = new FavouritesInValidator();
            ValidationResult results = validator.Validate(request);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return Errors.Favourites.FailedValidation;
            }

            return favouritesService.UpsertFavourites(id, request) == false ? Errors.Favourites.NotFound : true;
        }

        [HttpDelete("{id}")]
        public ErrorOr<bool> DeleteFavourites(int id)
        {
            return favouritesService.DeleteFavourites(id) == false ? Errors.Favourites.NotFound : true;
        }

        [HttpDelete]
        public bool DeleteFavouritess([FromQuery] List<int> ids)
        {
            return favouritesService.DeleteFavouritess(ids);
        }
    }
}