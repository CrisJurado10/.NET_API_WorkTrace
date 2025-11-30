using FluentValidation;

namespace WorkTrace.Application.DTOs.ClientDTO.Information;

public class UpdateClientValidator : AbstractValidator<UpdateClientRequest>
{
    public UpdateClientValidator()
    {
        RuleFor(x => x.FullName)
            .Length(1, 64);
        RuleFor(x => x.DocumentNumber)
            .Length(10, 15);
        RuleFor(X => X.PhoneNumber)
            .Length(10, 14);
        RuleFor(x => x.Email)
            .EmailAddress()
            .Length(6, 128);
    }
}