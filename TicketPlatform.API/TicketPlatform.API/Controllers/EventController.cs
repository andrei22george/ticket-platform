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
    [Route("events")]
    public class EventController : ControllerBase
    {
        private readonly EventService eventService;
        private readonly ILogger<EventController> _logger;

        public EventController(ILogger<EventController> logger, EventService service, IMapper mapper)
        {
            _logger = logger;
            eventService = service;
        }

        [HttpGet]
        public IEnumerable<Event> Get([FromQuery] QueryParameters parameters)
        {
            return eventService.GetAllEvents(parameters);
        }

        [HttpGet("{id}")]
        public ErrorOr<Event> GetEventById(int id)
        {
            return eventService.GetEventById(id);
        }

        [HttpPost]
        public ErrorOr<int> CreateEvent([FromBody] EventIn request)
        {
            var validator = new EventInValidator();
            ValidationResult results = validator.Validate(request);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return Errors.Event.FailedValidation;
            }

            if (StringValidationHelper.IsURL(request.Thumbnail))
            {
                return Errors.Event.InvalidURL;
            }
            else
            {
                return eventService.InsertEvent(request);
            }
        }

        [HttpPut("{id}")]
        public ErrorOr<bool> UpsertEvent(int id, [FromBody] EventIn request)
        {
            var validator = new EventInValidator();
            ValidationResult results = validator.Validate(request);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return Errors.Event.FailedValidation;
            }

            if (StringValidationHelper.IsURL(request.Thumbnail))
            {
                return Errors.Event.InvalidURL;
            }
            else
            {
                return eventService.UpsertEvent(id, request) == false ? Errors.Event.NotFound : true;
            }
        }

        [HttpDelete("{id}")]
        public ErrorOr<bool> DeleteEvent(int id)
        {
            return eventService.DeleteEvent(id) == false ? Errors.Event.NotFound : true;
        }

        [HttpDelete]
        public bool DeleteEvents([FromQuery] List<int> ids)
        {
            return eventService.DeleteEvents(ids);
        }
    }
}