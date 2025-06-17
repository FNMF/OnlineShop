﻿using System.Security.Cryptography;
using System.Text;

namespace API.Common.Helpers
{
    public class AESHelper
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("AES_KEY")
        ?? throw new Exception("AES_KEY not found"));

        // IV 长度固定 16 字节，AES块大小
        private const int IvSize = 16;

        public static string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Key;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            aes.GenerateIV();  // 随机生成IV
            var iv = aes.IV;

            using var encryptor = aes.CreateEncryptor(aes.Key, iv);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            var cipherBytes = ms.ToArray();

            // 把IV放在密文前面一起存储
            var result = new byte[IvSize + cipherBytes.Length];
            Buffer.BlockCopy(iv, 0, result, 0, IvSize);
            Buffer.BlockCopy(cipherBytes, 0, result, IvSize, cipherBytes.Length);

            return Convert.ToBase64String(result);
        }

        public static string Decrypt(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            if (fullCipher.Length < IvSize)
                throw new ArgumentException("Invalid cipher text");

            // 提取IV
            var iv = new byte[IvSize];
            Buffer.BlockCopy(fullCipher, 0, iv, 0, IvSize);

            // 提取密文
            var cipherBytes = new byte[fullCipher.Length - IvSize];
            Buffer.BlockCopy(fullCipher, IvSize, cipherBytes, 0, cipherBytes.Length);

            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(cipherBytes);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}
