using FluentValidation;
using Nowadays.Core.Dtos;

namespace Nowadays.Service.Validations;

public class ProjectDtoValidator : AbstractValidator<ProjectDto>
{
    public ProjectDtoValidator()
    {
        RuleFor(x => x.Name)
               .NotNull().WithMessage("{PropertyName} *")
               .NotEmpty().WithMessage("{PropertyName} *");
    }
}
