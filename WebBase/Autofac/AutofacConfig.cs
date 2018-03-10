using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebBase.Filters;

namespace WebBase.Autofac
{
    public class AutofacConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //配置所有Controller为支持属性注入
            var assmbly = ThisAssembly.GetTypes();
            var controllers = assmbly.Where(ss => ss.FullName.StartsWith("WebBase.Controllers") && !ss.IsNested)
                                     .ToArray();
            
            builder.RegisterTypes(controllers)
                   .PropertiesAutowired();
        }
    }
}
