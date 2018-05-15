using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Castle.DynamicProxy;
using WebExtentions.Aspace;
using WebExtentions.DependencyInjection;

namespace SampleInterceptor.Config
{
    public class InterceptorConfiguration : AutofacBase
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Interceptors.SampleInterceptor>()
                .Named<IInterceptor>("sampleInterceptor")
                .PropertiesAutowired()
                .InstancePerLifetimeScope()
                .AsSelf();
        }
    }
}
