using System.Text.Json;

namespace CQRS.Extensions
{
    public class HttpMethodMiddleware
    {
        // Declaring a private field to store the next middleware delegate
        private readonly RequestDelegate _next;
        // Declaring a private field to store the allowed HTTP methods
        private readonly string[] _allowedMethods;
        // Constructor to initialize the middleware with the next delegate and allowed methods.
        public HttpMethodMiddleware(RequestDelegate next, string[] allowedMethods)
        {
            _next = next;  // Storing the next delegate in the pipeline.
            _allowedMethods = allowedMethods;  // Storing the allowed HTTP methods.
        }
        // Asynchronous method that processes each HTTP request.
        public async Task InvokeAsync(HttpContext context)
        {
            // Checks if the current HTTP method is not in the list of allowed methods.
            if (!_allowedMethods.Contains(context.Request.Method))
            {
                // Setting status code to 405.
                context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                // Setting the response content type to JSON.
                context.Response.ContentType = "application/json";
                // Creating an anonymous object to hold the error details.
                var customResponse = new
                {
                    Code = 405,  // HTTP status code for "Method Not Allowed".
                    Message = "HTTP Method not allowed"  // Custom error message.
                };
                // Serializing the custom response object to JSON.
                var responseJson = JsonSerializer.Serialize(customResponse);
                // Writing the serialized JSON to the HTTP response.
                await context.Response.WriteAsync(responseJson);
                return; // Short-circuiting the pipeline to prevent further processing.
            }
            // If the method is allowed, pass the context to the next middleware in the pipeline.
            await _next(context);
        }
    }
}
