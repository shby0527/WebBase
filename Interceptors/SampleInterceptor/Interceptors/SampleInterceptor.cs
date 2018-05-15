using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using WebExtentions.Aspace;

namespace SampleInterceptor.Interceptors
{
    public class SampleInterceptor : AspectBase
    {
        public ILogger<SampleInterceptor> Logger { get; set; }

        protected override void AfterProcess(IInvocation invocation)
        {
            Logger.LogInformation($"{invocation.TargetType.FullName}::{invocation.Method.Name} is called,returned {invocation.ReturnValue.ToString()}");
        }

        protected override bool BeforeProcess(IInvocation invocation)
        {
            Logger.LogInformation($"{invocation.TargetType.FullName}::{invocation.Method.Name} whill be call");
            return true;
        }
    }
}
