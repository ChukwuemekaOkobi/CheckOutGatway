using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared;
using Shared.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        protected string ApiKey =>  GetApiKey();

        /// <summary>
        /// Gets the ApiKey from the Request Header
        /// </summary>
        /// <returns></returns>
        private string GetApiKey()
        {
            HttpContext.Request.Headers.TryGetValue(StringKeys.ApiKey, out var ApiKey);
            return ApiKey;
        } 
        protected IActionResult ApiResult<T>(Result<T> result)
        {

            if (result.StatusCode == StringKeys.Fail)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }
    }

}
