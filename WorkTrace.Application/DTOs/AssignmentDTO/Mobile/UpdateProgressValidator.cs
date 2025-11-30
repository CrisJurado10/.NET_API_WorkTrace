using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace WorkTrace.Application.DTOs.AssignmentDTO.Mobile;

public class UpdateProgressValidator : AbstractValidator<UpdateProgressRequest>
{
    public UpdateProgressValidator()
    {
        //RuleFor(x => x.StepProgresses)
        //    .NotEmpty().WithMessage("Los pasos y el progreso no pueden ser nulos ni vacios");

        //RuleForEach(x => x.StepProgresses)
        //    .Must(step => step.IsCompleted || step.CompletedAt == null)
        //    .WithMessage("Se toma la hora solo si el paso se completo");

        RuleForEach(x => x.MediaFiles)
            .Must(BeAValidImage)
            .WithMessage("Subir al menos una imagen.");
    }

    private bool BeAValidImage(IFormFile file)
    {
        if (file == null) return false;
        if (file.Length == 0) return false;

        var validTypes = new[] { "image/jpeg", "image/png", "image/jpg" };

        return validTypes.Contains(file.ContentType);
    }
}