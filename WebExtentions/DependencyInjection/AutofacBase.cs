using Autofac;
using Autofac.Extras.DynamicProxy;
using System;

namespace WebExtentions.DependencyInjection
{
    public abstract class AutofacBase : Module
    {
        //此处重写避免产生异常
        protected override System.Reflection.Assembly ThisAssembly => this.GetType().Assembly;

        protected override void Load(ContainerBuilder builder)
        {
            //默认注册本程序集下所有的类为服务
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired()
                .EnableClassInterceptors()
                .EnableInterfaceInterceptors()
                .AsSelf();
        }
    }
}