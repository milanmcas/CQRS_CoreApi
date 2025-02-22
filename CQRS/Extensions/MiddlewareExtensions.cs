using CQRS.Middleware;
using CQRS.Models;

namespace CQRS.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseContentLengthRestriction(this IApplicationBuilder builder, ContentLengthRestrictionOptions contentLengthRestrictionOptions)
        => builder.UseMiddleware<ContentLengthRestrictionMiddleware>(contentLengthRestrictionOptions);
    }
}
