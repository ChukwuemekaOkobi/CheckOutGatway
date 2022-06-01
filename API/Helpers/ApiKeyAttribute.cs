

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Implementation;
using Shared;

namespace WebAPI.Helpers
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _auth = context.HttpContext.RequestServices.GetService<IAuthentication>();

            if (!context.HttpContext.Request.Headers.TryGetValue(StringKeys.ApiKey, out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key was not provided"
                };
                return;
            }

            //authenticate the merchant
            var isValid = _auth.Authenticate(extractedApiKey);

            if (!isValid)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key is not valid"
                };
                return;
            }

            await next();
        }
    }
}
