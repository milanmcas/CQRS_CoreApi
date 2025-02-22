using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Http.Controllers;
//using System.Web.Http.Filters;

namespace CQRS.Filters
{
    public class VerbAuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    {
        //public bool AllowMultiple => true;

        //public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        //{
        //    if (actionContext.Request.Method==HttpMethod.Get)
        //    {
                
        //    }
            
        //}

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.Method== HttpMethod.Get.Method && DateTime.Now.Hour>19)
            {
                context.Result =new StatusCodeResult(405);
            }
        }
    }
}
