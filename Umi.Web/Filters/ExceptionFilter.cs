using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Umi.Web.Filters
{
    public class ExceptionFilter : IFilterMetadata, IExceptionFilter, IAsyncExceptionFilter
    {
        private ILogger _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            this._logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            this.OnException(context);
            return Task.CompletedTask;
        }
    }
}
