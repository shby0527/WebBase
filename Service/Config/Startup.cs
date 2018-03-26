using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using WebExtentions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.EntityConfig;

namespace Service.Config
{
    [Startup]
    public class Startup
    {
        private ILoggerFactory _loggerFactory;

        public Startup(ILoggerFactory factory)
        {
            _loggerFactory = factory;
        }

        public void ConfigureService(IServiceCollection services, ILogger<Startup> logger, IConfiguration configuration)
        {
            //services.AddRouting();
            services.AddEntityFrameworkMySql();
            services.AddDbContextPool<EntityConfigContext>(
                ss => ss.UseMySql(configuration.GetConnectionString("Mysql"))
                                .UseLoggerFactory(_loggerFactory));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseMvc();
        }
    }
}