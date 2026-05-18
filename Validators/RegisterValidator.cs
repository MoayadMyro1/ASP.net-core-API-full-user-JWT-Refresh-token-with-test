using DriverApi.ModelsDtos;
using FluentValidation;

namespace DriverApi.Validators
{
    public class RegisterValidator
        : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full name is required")
                .MinimumLength(3);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^[0-9]+$")
                .WithMessage("Phone number must contain only numbers");
        }
    }
}