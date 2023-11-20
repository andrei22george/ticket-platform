using FluentValidation;
using TicketPlatformBackend.Model.In;

namespace TicketPlatformBackend.Utilities
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
