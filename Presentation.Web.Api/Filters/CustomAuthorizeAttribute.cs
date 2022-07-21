using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Web.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = (string) context.HttpContext.Items["currentUser"];
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new JsonResult(new
                {
                    message = "Unauthorized"
                }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}