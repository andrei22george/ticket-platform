using FluentValidation;
using TicketPlatform.API.Model.In;

namespace TicketPlatform.API.Utilities
{
    public class UserInValidator : AbstractValidator<UserIn>
    {
        public UserInValidator()
        {
            RuleFor(userIn => userIn.Name).NotNull();
            RuleFor(userIn => userIn.Email).NotNull();
            RuleFor(userIn => userIn.Password).NotNull();
        }
    }
}