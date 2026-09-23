using System;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public static class Md5Helpler
    {
        /// <summary>
        /// 计算字符串的 MD5 哈希值，返回32位小写十六进制字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ComputeHash(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input)); // nameof()可以将变量名转换成字符串
            }

            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // 转换为16进制字符串
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // x2 = 小写两位十六进制
                }

                return sb.ToString();
            }
        }
    }
}
