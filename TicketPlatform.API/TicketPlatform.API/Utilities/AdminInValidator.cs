using FluentValidation;
using TicketPlatform.API.Model.In;

namespace TicketPlatform.API.Utilities
{
    internal class AdminInValidator : AbstractValidator<AdminIn>
    {
        public AdminInValidator()
        {
            RuleFor(adminIn => adminIn.Name).NotNull();
            RuleFor(adminIn => adminIn.Email).NotNull();
            RuleFor(adminIn => adminIn.Password).NotNull();
        }
    }
}