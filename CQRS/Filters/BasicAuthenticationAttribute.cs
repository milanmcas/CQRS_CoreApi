using Polly;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CQRS.Filters
{
    public class BasicAuthenticationAttribute:AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var a = actionContext.Request.Headers.Authorization;
            var authHeader = actionContext.Request.Headers.Authorization;
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
            var username = credentials[0];
            var password = credentials[1];
            //actionContext.RequestContext.Principal.Identity.Name = username;
            //https://jasonwatmore.com/post/2021/12/20/net-6-basic-authentication-tutorial-with-example-api#authorize-attribute-cs
        }
    }
}
