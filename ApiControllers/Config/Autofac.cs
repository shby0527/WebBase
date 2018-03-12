using System;
using System.Linq;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebExtentions.DependencyInjection;

namespace Controllers.Config
{
    public class Autofac : AutofacBase
    {

        protected override void Load(ContainerBuilder builder)
        {
            var controllers = ThisAssembly.GetTypes().Where(ss => ss.FullName.StartsWith("Controllers.Controllers") && !ss.IsNested).ToArray();

            builder.RegisterTypes(controllers)
                          .PropertiesAutowired();
        }
    }
}