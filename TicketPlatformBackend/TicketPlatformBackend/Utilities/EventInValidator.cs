using FluentValidation;
using TicketPlatformBackend.Model.In;

namespace TicketPlatformBackend.Utilities
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
