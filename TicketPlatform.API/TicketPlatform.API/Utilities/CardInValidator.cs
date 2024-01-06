using FluentValidation;
using TicketPlatform.API.Model.In;

namespace TicketPlatform.API.Utilities
{
    public class CardInValidator : AbstractValidator<CardIn>
    {
        public CardInValidator()
        {
            RuleFor(cardIn => cardIn.Name).NotNull();
            RuleFor(cardIn => cardIn.CardNumber).NotNull();
            RuleFor(cardIn => cardIn.CVV).NotNull();
            RuleFor(cardIn => cardIn.ExpDate).NotNull();
        }
    }
}
