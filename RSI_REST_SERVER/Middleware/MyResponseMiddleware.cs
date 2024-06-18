namespace RSI_REST_SERVER.Middleware
{
    public class MyResponseMiddleware : IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Append("bozyheader", "mleko");
                return Task.CompletedTask;
            });

            await next(context);

        }
    }
}
