using System;

namespace Umi.Web.Abstraction.Cryptos
{
    /// <summary>
    /// 加密服务
    /// </summary>
    public interface ICryptoService
    {
        /// <summary>
        /// 加密函数名称
        /// </summary>
        /// <value></value>
        string Name { get; }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        byte[] Crypto(byte[] data, byte[] key);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">被加密数据</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        byte[] Decrypto(byte[] data, byte[] key);
    }
}
