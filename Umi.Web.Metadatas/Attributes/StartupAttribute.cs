using System;

namespace Umi.Web.Metadatas.Attributes
{
    /// <summary>
    ///  标记特性，标记为StartUp类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class StartupAttribute : Attribute
    {
    }
}
