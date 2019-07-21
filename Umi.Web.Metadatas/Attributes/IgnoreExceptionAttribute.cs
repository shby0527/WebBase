using System;

namespace Umi.Web.Metadatas.Attributes
{
    /// <summary>
    /// 忽略异常
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class IgnoreExceptionAttribute : Attribute
    {
    }
}
