using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebExtentions.DependencyInjection;
using SampleMiddleware.Middleware;

namespace SampleMiddleware.Configuration
{
    [Startup]
    public class Startup
    {
        private ILogger<Startup> _logger;

        public Startup(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger<Startup>();
        }

        public void ConfigureService(IServiceCollection services)
        {
            //services.AddRouting();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseMvc();
            app.UseSampleware();
        }
    }
}