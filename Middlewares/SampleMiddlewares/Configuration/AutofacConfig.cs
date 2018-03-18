using System;
using System.Linq;
using Autofac;
using WebExtentions.DependencyInjection;

namespace SampleMiddleware.Configuration
{
    public class AutofacConfig : AutofacBase
    {
        protected override void Load(ContainerBuilder builder)
        {
            var services = ThisAssembly.GetTypes();
            services = (from p in services
                        where !p.IsAbstract
                        && !p.IsInterface
                        && !p.IsNested
                        && p.FullName.StartsWith("SampleMiddleware.Services")
                        select p).ToArray();
                        
            builder.RegisterTypes(services)
                    .AsSelf()
                    .AsImplementedInterfaces()
                    .PropertiesAutowired()
                    .InstancePerLifetimeScope();
        }
    }
}