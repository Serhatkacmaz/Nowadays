using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Nowadays.Core.Dtos;
using Nowadays.Core.Models;
using Nowadays.Core.Services;

namespace Nowadays.Api.Filters;

public class NotFoundFilter<Entity, Dto> : IAsyncActionFilter where Entity : BaseEntity where Dto : class
{
    private readonly IService<Entity, Dto> _service;

    public NotFoundFilter(IService<Entity, Dto> service)
    {
        _service = service;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var idValue = context.ActionArguments.Values.FirstOrDefault();

        if (idValue is null)
        {
            await next();
            return;
        }

        var id = (int)idValue;
        var anyEntity = await _service.AnyAsync(x => x.Id == id);

        if (anyEntity.Data)
        {
            await next();
            return;
        }

        context.Result = new NotFoundObjectResult(NowadaysResponseDto<NoContentDto>.Fail(StatusCodes.Status404NotFound, $"{typeof(Entity).Name}({id}) not found"));
    }
}