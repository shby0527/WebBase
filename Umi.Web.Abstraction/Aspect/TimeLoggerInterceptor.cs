using System;
using System.Diagnostics;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;

namespace Umi.Web.Abstraction.Aspect
{
    public class TimeLoggerInterceptor : IInterceptor
    {
        private ILogger _logger;

        public TimeLoggerInterceptor(ILogger<TimeLoggerInterceptor> logger)
        {
            this._logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            Stopwatch sw = new Stopwatch();
            _logger.LogDebug("{}.{} starting running",
                invocation.TargetType.FullName,
                invocation.MethodInvocationTarget.Name);
            sw.Start();
            try
            {
                invocation.Proceed();
            }
            finally
            {
                sw.Stop();

                _logger.LogDebug("{}.{} running fininshed, using {} ms",
                invocation.TargetType.FullName,
                invocation.MethodInvocationTarget.Name,
                sw.ElapsedMilliseconds);
            }
        }
    }
}
