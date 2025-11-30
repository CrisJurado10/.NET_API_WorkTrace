using FluentValidation;

namespace WorkTrace.Application.DTOs.StatusDTO.Information;

public class UpdateStatusValidator : AbstractValidator<UpdateStatusRequest>
{
    public UpdateStatusValidator()
    {
        RuleFor(x => x.Name)
             .Length(1, 32);
        RuleFor(x => x.Description)
            .Length(10, 128);
        RuleFor(x => x.IsActive)
            .Must(value => value == true || value == false)
            .When(x => x.IsActive.HasValue);
    }
}