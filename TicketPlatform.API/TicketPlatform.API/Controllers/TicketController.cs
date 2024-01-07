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
    [Route("tickets")]
    public class TicketController : ControllerBase
    {
        private readonly TicketService ticketService;
        private readonly ILogger<TicketController> _logger;

        public TicketController(ILogger<TicketController> logger, TicketService service, IMapper mapper)
        {
            _logger = logger;
            ticketService = service;
        }

        [HttpGet]
        public IEnumerable<Ticket> Get([FromQuery] QueryParameters parameters)
        {
            return ticketService.GetAllTickets(parameters);
        }

        [HttpGet("{id}")]
        public ErrorOr<Ticket> GetTicketById(int id)
        {
            return ticketService.GetTicketById(id);
        }

        [HttpPost]
        public ErrorOr<int> CreateTicket([FromBody] TicketIn request)
        {
            var validator = new TicketInValidator();
            ValidationResult results = validator.Validate(request);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return Errors.Ticket.FailedValidation;
            }

            if (StringValidationHelper.IsURL(request.QRCode))
            {
                return Errors.Ticket.InvalidQRCode;
            }
            else
            {
                return ticketService.InsertTicket(request);
            }
        }

        [HttpPut("{id}")]
        public ErrorOr<bool> UpsertTicket(int id, [FromBody] TicketIn request)
        {
            var validator = new TicketInValidator();
            ValidationResult results = validator.Validate(request);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return Errors.Ticket.FailedValidation;
            }

            if (StringValidationHelper.IsURL(request.QRCode))
            {
                return Errors.Ticket.InvalidQRCode;
            }
            else
            {
                return ticketService.UpsertTicket(id, request) == false ? Errors.Ticket.NotFound : true;
            }
        }

        [HttpDelete("{id}")]
        public ErrorOr<bool> DeleteTicket(int id)
        {
            return ticketService.DeleteTicket(id) == false ? Errors.Ticket.NotFound : true;
        }

        [HttpDelete]
        public bool DeleteTickets([FromQuery] List<int> ids)
        {
            return ticketService.DeleteTickets(ids);
        }
    }
}
