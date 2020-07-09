using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreSingletonControllerHttpContext
{
    public class PingController : ControllerBase
    {
        [HttpGet]
        public string Echo()
        {
            try
            {
                HttpContext.Response.Headers.Add("X", "Y");
            }
            catch (Exception ex)
            {
                // Set breakpoint here:
                throw;
            }

            return "ABC";
        }
    }
}
