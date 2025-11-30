using FluentValidation;

namespace WorkTrace.Application.DTOs.AssignmentDTO.Mobile;

public class UpdateLocationValidator : AbstractValidator<UpdateLocationRequest>
{
    public UpdateLocationValidator()
    {
        RuleFor(x => x.CurrentLocation)
                    .NotNull().WithMessage("La ubicación actual es necesaria.")
                    .Must(loc => loc.Latitude != 0 && loc.Longitude != 0)
                    .WithMessage("La ubicación actual deben estar en formato de coordenadas.");
    }
}