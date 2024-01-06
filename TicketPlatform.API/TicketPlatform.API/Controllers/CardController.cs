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
    [Route("card")]
    public class CardController : ControllerBase
    {
        private readonly CardService cardService;
        private readonly ILogger<CardController> _logger;

        public CardController(ILogger<CardController> logger, CardService service, IMapper mapper)
        {
            _logger = logger;
            cardService = service;
        }

        [HttpGet]
        public IEnumerable<Card> Get([FromQuery] QueryParameters parameters)
        {
            return cardService.GetAllCards(parameters);
        }

        [HttpGet("{id}")]
        public ErrorOr<Card> GetCardById(int id)
        {
            return cardService.GetCardById(id);
        }

        [HttpPost]
        public ErrorOr<int> CreateCard([FromBody] CardIn request)
        {
            var validator = new CardInValidator();
            ValidationResult results = validator.Validate(request);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return Errors.Card.FailedValidation;
            }

            if (StringValidationHelper.IsCardNumber(request.CardNumber))
            {
                return Errors.Card.InvalidCard;
            }
            else
            {
                return cardService.InsertCard(request);
            }
        }

        [HttpPut("{id}")]
        public ErrorOr<bool> UpsertCard(int id, [FromBody] CardIn request)
        {
            var validator = new CardInValidator();
            ValidationResult results = validator.Validate(request);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return Errors.Card.FailedValidation;
            }

            if (StringValidationHelper.IsCardNumber(request.CardNumber))
            {
                return Errors.Card.InvalidCard;
            }
            else
            {
                return cardService.UpsertCard(id, request) == false ? Errors.Card.NotFound : true;
            }
        }

        [HttpDelete("{id}")]
        public ErrorOr<bool> DeleteCard(int id)
        {
            return cardService.DeleteCard(id) == false ? Errors.Card.NotFound : true;
        }

        [HttpDelete]
        public bool DeleteCards([FromQuery] List<int> ids)
        {
            return cardService.DeleteCards(ids);
        }
    }
}