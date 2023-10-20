using FluentValidation;
using Nowadays.Core.Dtos;

namespace Nowadays.Service.Validations;

public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
{
    public EmployeeDtoValidator()
    {
        RuleFor(x => x.Name)
               .NotNull().WithMessage("{PropertyName} *")
               .NotEmpty().WithMessage("{PropertyName} *");
    }
}