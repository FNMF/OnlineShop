using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace API.Helpers
{
    public class RandomKeyHelper
    {
        public static string GetIpKey(string ip)
        {
            // 添加时间和随机因子作为扰动
            string salt = DateTime.UtcNow.Ticks.ToString() + Guid.NewGuid().ToString("N").Substring(0, 6);
            string input = ip + "|" + salt;

            using var sha = SHA256.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input));

            string base36 = ToBase36(hash);

            // 截取前 10 个字符
            return base36.Substring(0, 10);
        }

        private static string ToBase36(byte[] data)
        {
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";        //Base表仅包含大写字母和数字
            var result = new StringBuilder();

            BigInteger value = new BigInteger(data.Append((byte)0).ToArray());      // 防止负数

            while (value > 0)
            {
                value = BigInteger.DivRem(value, 36, out BigInteger remainder);
                result.Insert(0, chars[(int)remainder]);
            }

            return result.ToString();
        }
    }
}
