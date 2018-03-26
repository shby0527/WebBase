using System;
using System.Linq;
using Autofac;
using Entitys.Interface;
using Service.EntityConfig;
using WebExtentions.DependencyInjection;

namespace Service.Config
{
    public class Autofac : AutofacBase
    {
        protected override void Load(ContainerBuilder builder)
        {
            var services = from p in ThisAssembly.GetTypes() where p.FullName.StartsWith("Service.Services") && !p.IsNested select p;

            builder.RegisterTypes(services.ToArray())
                .AsImplementedInterfaces()
                .AsSelf()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            builder.RegisterType<EntityConfigContext>()
                .PropertiesAutowired()
                .InstancePerLifetimeScope()
                .AsSelf()
                .As<IDbPool>();
        }
    }
}