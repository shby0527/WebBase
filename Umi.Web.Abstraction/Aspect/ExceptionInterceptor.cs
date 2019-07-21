using System;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using Umi.Web.Metadatas.Attributes;

namespace Umi.Web.Abstraction.Aspect
{
    public class ExceptionInterceptor : IInterceptor
    {
        private ILogger _logger;

        public ExceptionInterceptor(ILogger<ExceptionInterceptor> logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            var attr = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(IgnoreExceptionAttribute), false);
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{}.{} running throws an Exception",
                invocation.TargetType.FullName,
                invocation.MethodInvocationTarget.Name);
                if (attr == null || attr.Length == 0)
                {
                    throw;
                }
                _logger.LogInformation("Ignore throw");
            }
        }
    }
}
