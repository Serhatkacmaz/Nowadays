using FluentValidation;
using Nowadays.Core.Dtos;

namespace Nowadays.Service.Validations;

public class IssueDtoValidator : AbstractValidator<IssueDto>
{
    public IssueDtoValidator()
    {
        RuleFor(x => x.Name)
               .NotNull().WithMessage("{PropertyName} *")
               .NotEmpty().WithMessage("{PropertyName} *");
    }
}
