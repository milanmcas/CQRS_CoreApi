namespace CQRS.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            // code executed before the next middleware
            Console.WriteLine("CustomMiddleware");
            await _next.Invoke(context); // call next middleware

            // code executed after the next middleware

        }
    }
    public class CustomMiddleware1
    {
        private readonly RequestDelegate _next;
        public CustomMiddleware1(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            // code executed before the next middleware
            Console.WriteLine(context.Request.Path.Value);
            Console.WriteLine("CustomMiddleware1"+context.Request.Method+context.Request.Host.Value);
            await _next.Invoke(context); // call next middleware

            // code executed after the next middleware

        }
    }
    public class CustomMiddleware2
    {
        private readonly RequestDelegate _next;
        public CustomMiddleware2(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            // code executed before the next middleware
            Console.WriteLine("CustomMiddleware2");
            await _next.Invoke(context); // call next middleware

            // code executed after the next middleware

        }
    }
}
