using Alachisoft.NCache.Common;
using CQRS.Security.Models;
using FluentValidation;

namespace CQRS.Security
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationRequest>
    {
        public UserRegistrationValidator()
        {
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required")
            .Length(1, 50).WithMessage("FirstName cannot be longer than 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
        }
    }
    public class UserValidator : AbstractValidator<UserRegistrationRequest>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstName)
              .NotEmpty()
              .WithMessage("First name is required.");
            RuleFor(user => user.LastName)
              .NotEmpty()
              .WithMessage("Last name is required.");
            RuleFor(user => user.Email)
              .EmailAddress()
              .WithMessage("Invalid email address.");
        }
    }
}
