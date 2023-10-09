using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UserWebAPI.Exceptions.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionFilter(IHostEnvironment hostEnvironment) =>
            _hostEnvironment = hostEnvironment;

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is UserNotFoundException)
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
                return;
            }

            if (context.Exception is RoleNotFoundException)
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
                return;
            }

            if (context.Exception is Exception)
            {
                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
