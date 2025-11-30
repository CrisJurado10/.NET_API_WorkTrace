using FluentValidation;


namespace WorkTrace.Application.DTOs.ServiceMgmtDTO.Management;

public class UpdateServiceValidator : AbstractValidator<UpdateServiceRequest>
{
    public UpdateServiceValidator()
    {
        RuleFor(x => x.Name)
              .NotEmpty()
              .Length(1, 128);

        RuleFor(x => x.Description)
            .NotEmpty()
            .Length(1, 256);

        RuleFor(x => x.InstallationSteps)
            .NotNull().WithMessage("La lista de pasos no puede estar nula ni vacía")
            .NotEmpty().WithMessage("Al menos un paso es requerido")
            .Must(steps => steps == null || steps.Select(s => s.Steps).Distinct().Count() == steps.Count)
            .WithMessage("Los números de los pasos deben ser únicos");

        RuleForEach(x => x.InstallationSteps)
            .SetValidator(new UpdateInstallationStepValidator());
    }
}
