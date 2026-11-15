using System.Diagnostics;

namespace StudentCourseManagment.Middleware
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimingMiddleware> _logger;

        public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            await _next(context);
            stopwatch.Stop();
            var correlationId = context.Items[CorrelationIdMiddleware.CorrelationIdHeader];
            _logger.LogInformation("{Method} {Path} [{CorrelationId}] -> {statuscode} in {Elapsed} ms", context.Request.Method, context.Request.Path,correlationId, context.Response.StatusCode, stopwatch.ElapsedMilliseconds);
        }
    }

        public static class RequestTimingMiddlewareExtensions
        {
            public static IApplicationBuilder UseRequestTiming(this IApplicationBuilder app)
            {
                return app.UseMiddleware<RequestTimingMiddleware>();
            }
        }
}

