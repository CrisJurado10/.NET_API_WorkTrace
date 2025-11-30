using FluentValidation;

namespace WorkTrace.Application.DTOs.AssignmentDTO.Management;

public class CreateAssignmentValidator : AbstractValidator<CreateAssignmentRequest>
{
    public CreateAssignmentValidator()
    {
        RuleFor(x => x.Users)
            .NotEmpty().WithMessage("No se puede agendar una Asignación sin Usuarios");

        RuleForEach(x => x.Users)
            .NotEmpty().WithMessage("El Id del Usuario no puede ser vacío")
            .Must(BeValidObjectId).WithMessage("El Id de un Usuario debe ser válido");

        RuleFor(x => x.Service)
            .NotEmpty().WithMessage("El Id del Servicio no puede ser vacío")
            .Must(BeValidObjectId).WithMessage("El Id de un Servicio debe ser válido");

        RuleFor(x => x.Client)
            .NotEmpty().WithMessage("El Id de un Cliente no puede ser vacío")
            .Must(BeValidObjectId).WithMessage("El Id de un Cliente debe ser válido");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("El Id del estatus es requerido")
            .Must(BeValidObjectId).WithMessage("El Status debe tener un Id Válido");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("La ubicación es requerida")
            .MinimumLength(5).WithMessage("La ubicación tiene que tener más de 5 carácteres");

        RuleFor(x => x.AssignedDate)
            .NotEmpty().WithMessage("La Fecha donde se dará el Servicio es obligatoria")
            .GreaterThan(DateTime.MinValue);

        RuleFor(x => x.CreatedByUser)
            .NotEmpty().WithMessage("El Usuario que creo la Asignación debe ser Administrador validado.")
            .Must(BeValidObjectId).WithMessage("El Id del Usuario que creo la Asignación debe existir.");
    }
    private bool BeValidObjectId(string id)
    {
        return MongoDB.Bson.ObjectId.TryParse(id, out _);
    }
}
