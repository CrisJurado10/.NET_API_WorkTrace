using FluentValidation;

namespace WorkTrace.Application.DTOs.AssignmentDTO.Management;

public class UpdateAssignmentWebValidator : AbstractValidator<UpdateAssignmentWebRequest>
{
    public UpdateAssignmentWebValidator()
    {

        // Validar lista de usuarios si se envía
        RuleForEach(x => x.Users)
            .Must(BeValidObjectId).WithMessage("El Id del Usuario debe ser válido");

        // Validar Service si se envía
        RuleFor(x => x.Service)
            .Must(BeValidObjectId).When(x => !string.IsNullOrEmpty(x.Service))
            .WithMessage("El Id del Servicio debe ser válido");

        // Validar Client si se envía
        RuleFor(x => x.Client)
            .Must(BeValidObjectId).When(x => !string.IsNullOrEmpty(x.Client))
            .WithMessage("El Id del Cliente debe ser válido");

        // Validar Status si se envía
        RuleFor(x => x.Status)
            .Must(BeValidObjectId).When(x => !string.IsNullOrEmpty(x.Status))
            .WithMessage("El Id del Status debe ser válido");

        // Validar Address si se envía
        RuleFor(x => x.Address)
            .MinimumLength(5).When(x => !string.IsNullOrEmpty(x.Address))
            .WithMessage("La dirección debe tener más 5 letras");

        // Validar AssignedDate si se envía
        RuleFor(x => x.AssignedDate)
            .GreaterThan(DateTime.MinValue).When(x => x.AssignedDate.HasValue)
            .WithMessage("La fecha de Asignación debe ser una fecha válida.");

        // Validar CreatedByUser si se envía
        RuleFor(x => x.CreatedByUser)
            .Must(BeValidObjectId).When(x => !string.IsNullOrEmpty(x.CreatedByUser))
            .WithMessage("El Id del Usuario que creo la asignación debe ser válido");
    }

    private bool BeValidObjectId(string id)
    {
        return MongoDB.Bson.ObjectId.TryParse(id, out _);
    }

}
