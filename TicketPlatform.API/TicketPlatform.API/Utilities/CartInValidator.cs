using FluentValidation;
using TicketPlatform.API.Model.In;

namespace TicketPlatform.API.Utilities
{
    public class CartInValidator : AbstractValidator<CartIn>
    {
        public CartInValidator()
        {
            RuleFor(cartIn => cartIn.IdEvent).NotNull();
            RuleFor(cartIn => cartIn.IdUser).NotNull();
            RuleFor(cartIn => cartIn.TicketsNumber).NotNull();
        }
    }
}
