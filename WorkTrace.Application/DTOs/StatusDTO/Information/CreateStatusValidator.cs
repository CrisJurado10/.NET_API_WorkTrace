using FluentValidation;

namespace WorkTrace.Application.DTOs.StatusDTO.Information;

public class CreateStatusValidator : AbstractValidator<CreateStatusRequest>
{
    public CreateStatusValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(1, 32);
        RuleFor(x => x.Description)
            .NotEmpty()
            .Length(10, 128);
        RuleFor(x => x.IsActive)
            .NotEmpty()
            .Must(value => value == true || value == false);
    }
}