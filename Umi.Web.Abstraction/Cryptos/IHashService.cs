using System;

namespace Umi.Web.Abstraction.Cryptos
{
    /// <summary>
    /// Hash 计算服务
    /// </summary>
    public interface IHashService
    {
        /// <summary>
        /// Hash 名字
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 计算Hash
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>返回</returns>
        byte[] ComputeHash(byte[] data);
    }
}
