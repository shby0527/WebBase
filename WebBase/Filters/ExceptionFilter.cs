using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace WebBase.Filters
{
    public class ExceptionFilter : IFilterMetadata, IAsyncExceptionFilter, IExceptionFilter
    {
        private ILogger logger;

        private IHostingEnvironment _environment;

        public ExceptionFilter(ILogger<ExceptionFilter> factory, IHostingEnvironment env)
        {
            logger = factory;
            _environment = env;
        }
        public void OnException(ExceptionContext context)
        {
            logger.LogError($"action {context.ActionDescriptor.DisplayName}  throw an exception");

            context.Result = new ContentResult()
            {
                StatusCode = 500,
                Content = context.Exception.Message,
                ContentType = "text/html"
            };

            logger.LogError(context.Exception, "throw excepton");
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            return Task.Run(() => OnException(context));
        }
    }
}