using FluentValidation;
using TicketPlatformBackend.Model.In;

namespace TicketPlatformBackend.Utilities
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
