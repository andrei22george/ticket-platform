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
        }
    }
}