using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;

namespace WebBase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            NLogBuilder.ConfigureNLog("nlog.config");
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(ss =>
                {
                    ss.SetMinimumLevel(LogLevel.Trace);
                }).UseStartup<Startup>()
                .UseNLog()
                .Build();
        }
    }
}
