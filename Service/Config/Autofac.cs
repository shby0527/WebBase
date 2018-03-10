using System;
using System.Linq;
using Autofac;

namespace Service.Config
{
    public class Autofac : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var services = from p in ThisAssembly.GetTypes() where p.FullName.StartsWith("Service.Services") && !p.IsNested select p;

            builder.RegisterTypes(services.ToArray())
                .AsImplementedInterfaces()
                .AsSelf()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();
        }
    }
}