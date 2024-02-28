using Common.Helper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using DTO.Request;

namespace Presentation.Authorization
{
    [AttributeUsage(AttributeTargets.All)]
    public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext authorizationFilterContext)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("Action", "login");
            routeValues.Add("Controller", "Authorize");
            var isAjax = authorizationFilterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (authorizationFilterContext.HttpContext.Request.QueryString.HasValue)
            {
               if(authorizationFilterContext.HttpContext.Request.QueryString.Value.Contains("startRow"))
                {
                    isAjax = true;
                }
            }

            //if (authorizationFilterContext != null)
            //{
            //    if (CommonHelper.GetValuesFromSession<UserRequest>(authorizationFilterContext.HttpContext, "LoggedInUserDetails", "ThisismySecretKey") != null)
            //    {
            //        return;
            //    }
            //    else
            //    {
            //        if (isAjax)
            //        {
            //            authorizationFilterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
            //        }
            //        else
            //        {
            //            authorizationFilterContext.Result = new RedirectToRouteResult(routeValues);
            //        }
            //    }
            //}
            //else
            //{
            //    authorizationFilterContext.Result = new RedirectToRouteResult(routeValues);
            //}

        }
    }
}
