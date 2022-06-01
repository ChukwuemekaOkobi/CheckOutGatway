using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    public class ModelValidationFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    Errors = context.ModelState.Values.SelectMany(c => c.Errors).Select(c => c.ErrorMessage).ToList(),
                    Message = "Information Invalid",
                    Code = StatusCodes.Status400BadRequest

                });

            }
        }


    }
}
