using Application.Common.DTO;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

namespace Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception exceptionObj)
            {
                await HandleExceptionAsync(context, exceptionObj);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string result = new ErrorDetails() { ErrorDescription = exception.Message, StatusCode = (int)HttpStatusCode.InternalServerError }.ToString();
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(result);
        }
    }
}
