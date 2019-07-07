using System;

namespace Umi.Web.Metadatas.Configurations
{
    /// <summary>
    /// 跨域资源访问的配制存放类
    /// </summary>
    public sealed class CorsConfiguration
    {
        /// <summary>
        /// 可导出的Http响应头
        /// </summary>
        public string[] ExposedHeaders { get; set; }

        /// <summary>
        /// 允许提交的请求头
        /// </summary>
        public string[] Headers { get; set; }

        /// <summary>
        /// 允许使用的Http 请求方法
        /// </summary>
        public string[] Methods { get; set; }

        /// <summary>
        /// 允许访问的Http 源
        /// </summary>
        public string[] Origins { get; set; }
    }
}
