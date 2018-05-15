using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;

namespace WebExtentions.Aspace
{
    /// <summary>
    /// aspect base
    /// </summary>
    public abstract class AspectBase : IInterceptor
    {
        /// <summary>
        /// base intercept
        /// </summary>
        /// <param name="invocation"></param>
        public virtual void Intercept(IInvocation invocation)
        {
            if (BeforeProcess(invocation))
            {
                invocation.Proceed();
                AfterProcess(invocation);
            }
        }

        /// <summary>
        /// default befor target called,if true , call the next invocation to target
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        protected abstract bool BeforeProcess(IInvocation invocation);

        /// <summary>
        /// default after target called
        /// </summary>
        /// <param name="invocation"></param>
        protected abstract void AfterProcess(IInvocation invocation);
    }
}
