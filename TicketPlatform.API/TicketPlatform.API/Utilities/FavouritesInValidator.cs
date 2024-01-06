using FluentValidation;
using TicketPlatform.API.Model.In;

namespace TicketPlatform.API.Utilities
{
    public class FavouritesInValidator : AbstractValidator<FavouritesIn>
    {
        public FavouritesInValidator()
        {
            RuleFor(favouritesIn => favouritesIn.Title).NotNull();
            RuleFor(favouritesIn => favouritesIn.Description).NotNull();
            RuleFor(favouritesIn => favouritesIn.Thumbnail).NotNull();
            RuleFor(favouritesIn => favouritesIn.Date).NotNull();
        }
    }
}
