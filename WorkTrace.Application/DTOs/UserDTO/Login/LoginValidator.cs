using FluentValidation;

namespace WorkTrace.Application.DTOs.UserDTO.Login;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty()
            .Length(8,16);
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .Length(6,128);
    }
}