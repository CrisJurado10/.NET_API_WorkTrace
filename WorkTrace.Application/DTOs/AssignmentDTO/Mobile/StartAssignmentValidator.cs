using FluentValidation;

namespace WorkTrace.Application.DTOs.AssignmentDTO.Mobile;

public class StartAssignmentValidator :AbstractValidator<StartAssigmentRequest>
{

    public StartAssignmentValidator()
    {
        RuleFor(x => x.CheckIn)
            .NotEmpty().WithMessage("El Tiempo del CheckIn es requerido.");

        RuleFor(x => x.CurrentLocation)
            .NotNull().WithMessage("La ubicación Actual es necesaria")
            .Must(loc => loc.Latitude != 0 && loc.Longitude != 0)
            .WithMessage("La Ubicación Actual debe estar en formato de coordenadas");
    }
}