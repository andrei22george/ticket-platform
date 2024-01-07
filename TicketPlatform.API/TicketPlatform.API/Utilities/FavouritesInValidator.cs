using FluentValidation;
using TicketPlatform.API.Model.In;

namespace TicketPlatform.API.Utilities
{
    public class FavouritesInValidator : AbstractValidator<FavouritesIn>
    {
        public FavouritesInValidator()
        {
            RuleFor(favouritesIn => favouritesIn.UserId).NotNull();
            RuleFor(favouritesIn => favouritesIn.AdminId).NotNull();
            RuleFor(favouritesIn => favouritesIn.EventId).NotNull();
        }
    }
}
