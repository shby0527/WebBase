using System;
using Autofac;

namespace Umi.Web.Abstraction.DependencyInject
{
    /// <summary>
    /// 依赖注入基类
    /// </summary>
    public class DependencyInjectBase : Module
    {
        protected override System.Reflection.Assembly ThisAssembly
        {
            get
            {
                return this.GetType().Assembly;
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            
        }
    }
}
