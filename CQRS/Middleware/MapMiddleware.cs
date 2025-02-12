namespace CQRS.Middleware
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder.Extensions;
    using Microsoft.AspNetCore.Http;
    public class MapMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MapOptions _options;

        /// <summary>
        /// https://github.com/dotnet/aspnetcore/tree/425c196cba530b161b120a57af8f1dd513b96f67/src/Http/Http.Abstractions/src/Extensions
        /// Creates a new instance of <see cref="MapMiddleware"/>.
        /// </summary>
        /// <param name="next">The delegate representing the next middleware in the request pipeline.</param>
        /// <param name="options">The middleware options.</param>
        public MapMiddleware(RequestDelegate next, MapOptions options)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _next = next;
            _options = options;
        }
        /// <summary>
        /// Executes the middleware.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> for the current request.</param>
        /// <returns>A task that represents the execution of this middleware.</returns>
        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            PathString matchedPath;
            PathString remainingPath;

            if (context.Request.Path.StartsWithSegments(_options.PathMatch, out matchedPath, out remainingPath))
            {
                // Update the path
                var path = context.Request.Path;
                var pathBase = context.Request.PathBase;
                context.Request.PathBase = pathBase.Add(matchedPath);
                context.Request.Path = remainingPath;

                try
                {
                    await _options.Branch!(context);
                }
                finally
                {
                    context.Request.PathBase = pathBase;
                    context.Request.Path = path;
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
