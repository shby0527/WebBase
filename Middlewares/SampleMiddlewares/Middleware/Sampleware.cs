using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SampleMiddleware.Middleware
{
    public class Sampleware
    {
        private RequestDelegate _next;
        private ILogger<Sampleware> _logger;

        public Sampleware(RequestDelegate next, ILoggerFactory factory)
        {
            _next = next;
            _logger = factory.CreateLogger<Sampleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
        }
    }
}