using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace WebApplicationWebAPI.Filters
{
    public class MyActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Debug.WriteLine("Before Action");

            ActionExecutedContext result = await next();

            if (result.Exception != null)
            {
                Debug.WriteLine($"Error: {result.Exception}");
            }
            else
            {
                Debug.WriteLine("Finish Action");
            }
        }
    }
}
