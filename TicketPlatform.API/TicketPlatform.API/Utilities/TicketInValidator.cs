using FluentValidation;
using TicketPlatform.API.Model.In;

namespace TicketPlatform.API.Utilities
{
    internal class TicketInValidator : AbstractValidator<TicketIn>
    {
        public TicketInValidator()
        {
            RuleFor(ticketIn => ticketIn.EventTitle).NotNull();
            RuleFor(ticketIn => ticketIn.EventDescription).NotNull();
            RuleFor(ticketIn => ticketIn.EventThumbnail).NotNull();
            RuleFor(ticketIn => ticketIn.EventDate).NotNull();
            RuleFor(ticketIn => ticketIn.Price).NotNull();
            RuleFor(ticketIn => ticketIn.QRCode).NotNull();
        }
    }
}