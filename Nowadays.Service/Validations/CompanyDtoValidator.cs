using FluentValidation;
using Nowadays.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nowadays.Service.Validations;

public class CompanyDtoValidator:AbstractValidator<CompanyDto>
{
    public CompanyDtoValidator()
    {
        RuleFor(x => x.Name)
               .NotNull().WithMessage("{PropertyName} *")
               .NotEmpty().WithMessage("{PropertyName} *");
    }
}
