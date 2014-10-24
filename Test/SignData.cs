
using System;
using System.Text;
using System.Security.Cryptography;

namespace RSACryptoServiceProvider_Examples
{
    class MyMainClass
    {
        static void Main()
        {
            using (var rsa = new RSACryptoServiceProvider())
            using (var sha1 = SHA1.Create())
            {
                //原始数据
                var data = new byte[] { 1, 2, 3 };
                //计算哈希值
                var hash = sha1.ComputeHash(data);

                //用SignHash，注意使用CryptoConfig.MapNameToOID
                var sigHash = rsa.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
                //用SignData，直接传入数据（函数内部会计算哈希值）
                var sigData = rsa.SignData(data, typeof(SHA1));

                //输出两个签名数据
                Console.WriteLine(BitConverter.ToString(sigHash));
                Console.WriteLine(BitConverter.ToString(sigData));

                //验证
                Console.WriteLine(rsa.VerifyHash(hash, "SHA1", sigHash));
                Console.WriteLine(rsa.VerifyData(data, typeof(SHA1), sigData));
            }
        }
    }

  
    
}