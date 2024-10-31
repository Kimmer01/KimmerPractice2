using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace WebApplicationWebAPI.Filters
{
    public class LogExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            Debug.WriteLine(context.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
