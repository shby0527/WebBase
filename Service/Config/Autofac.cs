using System;
using System.Linq;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Entitys.Interface;
using Service.EntityConfig;
using WebExtentions.DependencyInjection;

namespace Service.Config
{
    public class Autofac : AutofacBase
    {
        protected override void Load(ContainerBuilder builder)
        {
            //var services = from p in ThisAssembly.GetTypes() where p.FullName.StartsWith("Service.Services") && !p.IsNested select p;

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(p => p.FullName.StartsWith("Service.Services") && !p.IsNested)
                .AsImplementedInterfaces()
                .AsSelf()
                .PropertiesAutowired()
                .InstancePerLifetimeScope()
                .InterceptedBy("sampleInterceptor")
                .EnableClassInterceptors();

            builder.RegisterType<EntityConfigContext>()
                .PropertiesAutowired()
                .InstancePerLifetimeScope()
                .AsSelf()
                .As<IDbPool>();
        }
    }
}