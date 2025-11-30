using FluentValidation;

namespace WorkTrace.Application.DTOs.AssignmentDTO.Mobile;

public class FinishAssignmentValidator : AbstractValidator<FinishAssignmentRequest>
{
    public FinishAssignmentValidator()
    {
        RuleFor(x => x.CheckOut)
                    .NotEmpty().WithMessage("El Tiempo del CheckOut es Requerido");
    }
}