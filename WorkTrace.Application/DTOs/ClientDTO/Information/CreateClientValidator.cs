using FluentValidation;

namespace WorkTrace.Application.DTOs.ClientDTO.Information;

public class CreateClientValidator : AbstractValidator<CreateClientRequest>
{
    public CreateClientValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Ingrese su Nombre.")
            .Length(1, 64);
        RuleFor(x => x.DocumentNumber)
            .NotEmpty()
            .WithMessage("Documento no válido")
            .Length(10, 16);
        RuleFor(X => X.PhoneNumber)
            .NotEmpty()
            .WithMessage("Teléfono no válido")
            .Length(10, 14);
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Correo no valído.")
            .Length(6, 128);
    }
}