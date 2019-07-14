using System;

namespace Umi.Web.Metadatas.Exceptions
{
    /// <summary>
    ///  Http Status Exception
    /// </summary>
    public class HttpStatusException : SystemException
    {
        /// <summary>
        ///  Http 状态码
        /// </summary>
        /// <value></value>
        public int StatusCode { get; }

        /// <summary>
        ///  Http 状态信息
        /// </summary>
        /// <value></value>
        public string StatusMessage { get; }
    }
}
