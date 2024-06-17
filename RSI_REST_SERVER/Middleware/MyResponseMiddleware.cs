namespace RSI_REST_SERVER.Middleware
{
    public class MyResponseMiddleware
    {
        private readonly RequestDelegate _next;
        public MyResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                context.Request.Headers.Add("mojNaglowek", "rsi test");
                await _next(context);
            }
            catch(Exception ex)
            {
                ;
            }
            
        }
    }
}
