using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace webApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ExceptionHandlerConfiguration(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(
                options =>
                {
                    options.Run(
                         async context =>
                         {
                             context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                             var exception = context.Features.Get<IExceptionHandlerFeature>();
                             if (exception != null)
                             {
                                 await context.Response.WriteAsync(exception.Error.Message);
                             }
                         }
                    );
                }
            );
        }
    }
}
