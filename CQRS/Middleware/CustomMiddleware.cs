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
            Console.WriteLine(HttpMethod.Get.Method);
            Console.WriteLine(context.Request.Path.Value);
            Console.WriteLine("CustomMiddleware1 "+context.Request.Method+context.Request.Host.Value);
            if(context.Request.Method== HttpMethod.Get.Method)
            {
                Console.WriteLine("This is the get method");
                await _next.Invoke(context);
                //await context.Response.WriteAsync("This is the get method");

            }
            else
            {
                await _next.Invoke(context);
            }
            /*await _next.Invoke(context);*/ // call next middleware

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
    public class CustomMiddleware3
    {
        private readonly RequestDelegate _next;
        public CustomMiddleware3(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            // code executed before the next middleware
            Console.WriteLine(HttpMethod.Get.Method);
            Console.WriteLine(context.Request.Path.Value);
            Console.WriteLine("CustomMiddleware3 " + context.Request.Method + context.Request.Host.Value);
            if (context.Request.Method == HttpMethod.Get.Method)
            {
                Console.WriteLine("This is the get method");
                await _next.Invoke(context);
                //await context.Response.WriteAsync("This is the get method");

            }
            else
            {
                await _next.Invoke(context);
            }
            /*await _next.Invoke(context);*/ // call next middleware

            // code executed after the next middleware

        }
    }
}
