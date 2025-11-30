using FluentValidation;

namespace WorkTrace.Application.DTOs.UserDTO.Information;

public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.FullName)
            .Length(1, 64);
        RuleFor(X => X.PhoneNumber)
            .Length(10, 14);
        RuleFor(x => x.DocumentNumber)
            .Length(10, 15);
        RuleFor(x => x.Password)
            .Length(8, 16);
        RuleFor(x => x.Email)
            .EmailAddress()
            .Length(6, 128);
        RuleFor(x => x.IsActive)
            .Must(value =>  value == true || value == false)
            .When(x => x.IsActive.HasValue);
        RuleFor(x => x.Role)
            .Must(value => Enum.IsDefined(typeof(Enums.UserRoles), value))
            .When(x => x.Role.HasValue)
            .WithMessage("Rol inválido.");
    }
}