using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using WebExtentions.DependencyInjection;

namespace Controllers.Config
{
    [Startup]
    public class Startup
    {
        private ILoggerFactory _loggerFactory;

        public Startup(ILoggerFactory factory)
        {
            _loggerFactory = factory;
        }

        public void ConfigureService(IServiceCollection services, ILogger<Startup> logger)
        {
            //services.AddRouting();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseMvc();
        }
    }
}