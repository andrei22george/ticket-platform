using AutoMapper;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TicketPlatform.API.Model.In;
using TicketPlatform.API.Model;
using TicketPlatform.API.Services;
using TicketPlatform.API.Utilities;
using FluentValidation.Results;

namespace TicketPlatform.API.Controllers
{
    [ApiController]
    [Route("cart")]
    public class CartController : ControllerBase
    {
        private readonly CartService cartService;
        private readonly ILogger<CartController> _logger;

        public CartController(ILogger<CartController> logger, CartService service, IMapper mapper)
        {
            _logger = logger;
            cartService = service;
        }

        [HttpGet]
        public IEnumerable<Cart> Get([FromQuery] QueryParameters parameters)
        {
            return cartService.GetAllCarts(parameters);
        }

        [HttpGet("{id}")]
        public ErrorOr<Cart> GetCartById(int id)
        {
            return cartService.GetCartById(id);
        }

        [HttpPost]
        public ErrorOr<int> CreateCart([FromBody] CartIn request)
        {
            var validator = new CartInValidator();
            ValidationResult results = validator.Validate(request);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }

            return cartService.InsertCart(request);
        }

        [HttpPut("user={idUser}&event={idEvent}")]
        public ErrorOr<int> UpsertCart(int idUser, int idEvent, [FromBody] int ticketsNumber)
        {
            return cartService.UpsertCart(idUser, idEvent, ticketsNumber);
        }

        [HttpDelete("user={idUser}&event={idEvent}")]
        public ErrorOr<bool> DeleteCart(int idUser, int idEvent)
        {
            return cartService.DeleteCart(idUser, idEvent);
        }

        [HttpDelete]
        public bool DeleteCarts([FromQuery] List<int> ids)
        {
            return cartService.DeleteCarts(ids);
        }

        [HttpGet("email")]
        public void SendEmail([FromQuery] string email)
        {
            cartService.SendEmail(email);
        }
    }
}