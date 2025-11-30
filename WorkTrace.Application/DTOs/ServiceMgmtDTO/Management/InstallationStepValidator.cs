using FluentValidation;

namespace WorkTrace.Application.DTOs.ServiceMgmtDTO.Management;

public class InstallationStepValidator : AbstractValidator<InstallationStepResponse>
{
    public InstallationStepValidator()
    {
        RuleFor(x => x.Steps)
            .NotEmpty()
            .GreaterThan(0).WithMessage("El número del Paso debe ser mayor que 0");
        RuleFor(x => x.Description)
            .NotEmpty()
            .Length(1, 256);
    }
}