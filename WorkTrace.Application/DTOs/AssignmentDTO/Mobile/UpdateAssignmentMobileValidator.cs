using FluentValidation;

namespace WorkTrace.Application.DTOs.AssignmentDTO.Mobile;

public class UpdateAssignmentMobileValidator : AbstractValidator<UpdateAssignmentMobileRequest>
{
    public UpdateAssignmentMobileValidator()
    {
        RuleFor(x => x.CheckOut)
            .Must((request, checkOut) => !checkOut.HasValue || request.CheckIn.HasValue)
            .WithMessage("La hora del CheckOut no puede agregarse sin tener una hora de CheckIn puesta anteriormente");

        RuleFor(x => x.CurrentLocation)
            .Must(loc => loc.Latitude != 0 && loc.Longitude != 0)
            .When(x => x.CurrentLocation != null)
            .WithMessage("La ubicación actual debe estar en formato de coordenadas");

        //RuleForEach(x => x.StepsProgress)
        //    .Must(step => step.CompletedAt == null || step.IsCompleted)
        //    .WithMessage("Se toma la hora solo si el paso se completo");

        RuleForEach(x => x.MediaFiles)
            .Must(file => !string.IsNullOrEmpty(file.Url))
            .WithMessage("Subir al menos una imagen.");
    }
}