namespace CQRS.Middleware
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;

        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            // code executed before the next middleware
            Console.WriteLine("MyMiddleware");
            await _next.Invoke(context); // call next middleware

            // code executed after the next middleware

        }
    }
    public class MyMiddleware1
    {
        private readonly RequestDelegate _next;

        public MyMiddleware1(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            // code executed before the next middleware
            Console.WriteLine("MyMiddleware1");
            await _next.Invoke(context); // call next middleware

            // code executed after the next middleware

        }
    }
    public class MyMiddleware2
    {
        private readonly RequestDelegate _next;

        public MyMiddleware2(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            // code executed before the next middleware
            Console.WriteLine("MyMiddleware2");
            await _next.Invoke(context); // call next middleware

            // code executed after the next middleware

        }
    }
}
