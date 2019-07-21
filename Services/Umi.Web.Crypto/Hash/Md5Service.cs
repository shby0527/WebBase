using System;
using Org.BouncyCastle.Crypto.Digests;
using Umi.Web.Abstraction.Cryptos;
using Umi.Web.Metadatas.Attributes;

namespace Umi.Web.Crypto.Hash
{
    [Service("MD5Hash")]
    public class Md5Service : IHashService
    {
        public string Name => "MD5";

        public byte[] ComputeHash(byte[] data)
        {
            MD5Digest md5 = new MD5Digest();
            byte[] output = new byte[md5.GetDigestSize()];
            md5.BlockUpdate(data, 0, data.Length);
            md5.DoFinal(output, 0);
            return output;
        }
    }
}
