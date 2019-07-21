using System;

namespace Umi.Web.Metadatas.Attributes
{
    /// <summary>
    /// 依赖注入标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class ServiceAttribute : Attribute
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        /// <value></value>
        public string Name { get; }

        public string[] Interceptors { get; }

        public ServiceAttribute(string name, params string[] interceptors)
        {
            this.Name = name;
            this.Interceptors = interceptors;
        }

        public ServiceAttribute()
        {
            this.Name = null;
        }
    }
}
