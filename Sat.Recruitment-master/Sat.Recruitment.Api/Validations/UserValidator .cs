using FluentValidation;
using Sat.Recruitment.Models.DTOs;
using Sat.Recruitment.Models.Models;

namespace Sat.Recruitment.Api.Validations
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(user => user.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required");
            RuleFor(user => user.Address).NotEmpty().WithMessage("Addres is required, can not be empty");
            RuleFor(user => user.Phone).Matches(@"^\+?\d{10,15}$").WithMessage("Phone number must be between 10 and 15 digits and may start with a '+'");
        }
    }
}