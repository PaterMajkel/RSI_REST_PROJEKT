using System.Net;

namespace RSI_REST_SERVER.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unexpected error occurred.");

            var statusCode = HttpStatusCode.InternalServerError;
            var response = $"An error occurred: {exception.Message}";

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(response);
        }
    }
}
