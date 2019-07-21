using System;

namespace Umi.Web.Abstraction.Cryptos
{
    /// <summary>
    /// 数字签名
    /// </summary>
    public interface IDigitalSignedService
    {
        /// <summary>
        /// 签名函数名
        /// </summary>
        /// <value></value>
        string Name { get; }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="key">密钥</param>
        /// <param name="hash">hash函数名</param>
        /// <returns></returns>
        byte[] Sign(byte[] data, byte[] key, string hash);

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="key">密钥</param>
        /// <param name="signed">签名数据</param>
        /// <param name="hash">hash函数名</param>
        /// <returns></returns>
        bool Verification(byte[] data, byte[] key, byte[] signed, string hash);
    }
}
