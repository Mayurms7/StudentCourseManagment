namespace StudentCourseManagment.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        public const string CorrelationIdHeader = "X-Correlation-Id";
        private readonly ILogger<CorrelationIdMiddleware> _logger;

        public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationID = Guid.NewGuid().ToString();
            
            context.Items[CorrelationIdHeader] = correlationID;

            context.Response.Headers[CorrelationIdHeader] = correlationID;
            _logger.LogInformation("Correlation {CorrelationId} assigned to {Path}",
                CorrelationIdHeader, context.Request.Path);

            await _next(context);
        }
    }

    public static class CorrelationIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CorrelationIdMiddleware>();
        }
    }
}
