using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreWebApi.Helpers
{
    public class TokenFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string token=context.HttpContext.Request.Headers["Authorization"]; 
        }
    }
}