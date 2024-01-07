using FluentValidation;
using TicketPlatform.API.Model.In;

namespace TicketPlatform.API.Utilities
{
    public class EventInValidator : AbstractValidator<EventIn>
    {
        public EventInValidator()
        {
            RuleFor(eventIn => eventIn.Title).NotNull();
            RuleFor(eventIn => eventIn.Description).NotNull();
            RuleFor(eventIn => eventIn.Thumbnail).NotNull();
            RuleFor(eventIn => eventIn.Date).NotNull();
            RuleFor(eventIn => eventIn.Price).NotNull();
            RuleFor(eventIn => eventIn.Venue).NotNull();
            RuleFor(eventIn => eventIn.City).NotNull();
            RuleFor(eventIn => eventIn.TotalTickets).NotNull();
        }
    }
}