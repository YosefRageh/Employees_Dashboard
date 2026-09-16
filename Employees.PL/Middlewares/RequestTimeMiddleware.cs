namespace Employees.PL.Middlewares
{
    public class RequestTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // OnStarting fires right before headers are sent — safe place to add a header
            context.Response.OnStarting(() =>
            {
                context.Response.Headers["X-Request-Time"] = requestTime;
                return Task.CompletedTask;
            });

            await _next(context); // pass control to the next middleware
        }
    }

    public static class RequestTimeMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestTime(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestTimeMiddleware>();
        }
    }
}