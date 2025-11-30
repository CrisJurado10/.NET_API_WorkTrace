using FluentValidation;

namespace WorkTrace.Application.DTOs.UserDTO.Information;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .Length(1, 64);
        RuleFor(x => x.DocumentNumber)
            .NotEmpty()
            .Length(10, 15);
        RuleFor(X => X.PhoneNumber)
            .NotEmpty()
            .Length(10, 14);
        RuleFor(x => x.Email)
            .EmailAddress()
            .Length(6, 128);
        RuleFor(x => x.Password)
            .NotEmpty()
            .Length(8, 16);
        RuleFor(x => x.Role)
            .Must(x => Enum.IsDefined(typeof(Enums.UserRoles), x))
            .WithMessage("'Role' debe ser un valor válido");
        RuleFor(x => x.IsActive)
            .NotEmpty()
            .Must(value => value == true || value == false);
    }
}