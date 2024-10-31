using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplicationWebAPI.Filters
{
    public class MyExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MyExceptionFilter(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            ObjectResult result = new ObjectResult(new { Environment = _webHostEnvironment.EnvironmentName, Message = context.Exception.ToString() });
            context.Result = result;
            //context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
