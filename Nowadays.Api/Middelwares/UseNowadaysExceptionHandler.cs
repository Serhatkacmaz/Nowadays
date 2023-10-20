using Microsoft.AspNetCore.Diagnostics;
using Nowadays.Core.Dtos;
using Nowadays.Service.Exceptions;
using System.Text.Json;

namespace Nowadays.Api.Middelwares;

public static class UseNowadaysExceptionHandler
{
    public static void UseNowadaysException(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(config =>
        {
            config.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var excepitonFeature = context.Features.Get<IExceptionHandlerFeature>();
                var statusCode = excepitonFeature.Error switch
                {
                    ClientSideException => StatusCodes.Status400BadRequest,
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = statusCode;

                var response = NowadaysResponseDto<NoContentDto>.Fail(statusCode, excepitonFeature.Error.Message);
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            });
        });
    }

}
