using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Nowadays.Core.Dtos;

namespace Nowadays.Api.Filters;

public class ValidateFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage)
                .ToList();

            context.Result = new BadRequestObjectResult(NowadaysResponseDto<NoContentDto>.Fail(StatusCodes.Status400BadRequest, errors));
        }
    }
}