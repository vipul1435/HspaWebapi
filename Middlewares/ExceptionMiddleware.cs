using webApi.Errors;

namespace webApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            } 
            catch (Exception ex)
            {
                ApiError response;
                if (env.IsDevelopment())
                {
                    response = new(500, ex.Message, ex.StackTrace?.ToString());
                } else
                {
                    response = new(500, ex.Message);

                }

                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application.json";
                await context.Response.WriteAsync(response.ToString());
            }
        }
    }
}
