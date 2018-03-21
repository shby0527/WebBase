using Autofac;
using System;

namespace WebExtentions.DependencyInjection
{
    public abstract class AutofacBase : Module
    {
        //此处重写避免产生异常
        protected override System.Reflection.Assembly ThisAssembly => this.GetType().Assembly;

        protected override void Load(ContainerBuilder builder)
        {

        }
    }
}