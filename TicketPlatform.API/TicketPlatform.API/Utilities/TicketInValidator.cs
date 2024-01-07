using FluentValidation;
using TicketPlatform.API.Model.In;

namespace TicketPlatform.API.Utilities
{
    internal class TicketInValidator : AbstractValidator<TicketIn>
    {
        public TicketInValidator()
        {
            RuleFor(ticketIn => ticketIn.UserId).NotNull();
            RuleFor(ticketIn => ticketIn.AdminId).NotNull();
            RuleFor(ticketIn => ticketIn.EventId).NotNull();
            RuleFor(ticketIn => ticketIn.QRCode).NotNull();
        }
    }
}