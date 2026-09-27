using System;
using System.Security.Cryptography;

namespace Common
{
    /// <summary>
    /// 登录密码哈希。每个密码使用独立随机盐，并经过多次迭代，数据库中不保存明文。
    /// </summary>
    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;
        private const string Prefix = "PBKDF2";

        /// <summary>
        /// 生成可直接保存到数据库的密码字符串。
        /// </summary>
        public static string Hash(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("密码不能为空");
            }

            byte[] salt = new byte[SaltSize];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            byte[] hash = Derive(password, salt, Iterations);
            return Prefix + "$" + Iterations + "$" + Convert.ToBase64String(salt) + "$" + Convert.ToBase64String(hash);
        }

        /// <summary>
        /// 校验输入密码。比较时不提前返回，避免通过耗时差异猜测密码。
        /// </summary>
        public static bool Verify(string password, string storedHash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedHash))
            {
                return false;
            }

            string[] parts = storedHash.Split('$');
            int iterations;
            if (parts.Length != 4 || parts[0] != Prefix || !int.TryParse(parts[1], out iterations) || iterations < 1)
            {
                return false;
            }

            byte[] salt;
            byte[] expected;
            try
            {
                salt = Convert.FromBase64String(parts[2]);
                expected = Convert.FromBase64String(parts[3]);
            }
            catch (FormatException)
            {
                return false;
            }

            byte[] actual = Derive(password, salt, iterations);
            return FixedTimeEquals(actual, expected);
        }

        private static byte[] Derive(string password, byte[] salt, int iterations)
        {
            using (Rfc2898DeriveBytes derive = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return derive.GetBytes(HashSize);
            }
        }

        private static bool FixedTimeEquals(byte[] left, byte[] right)
        {
            int length = left == null ? 0 : left.Length;
            int difference = length ^ (right == null ? 0 : right.Length);
            for (int i = 0; i < length && right != null && i < right.Length; i++)
            {
                difference |= left[i] ^ right[i];
            }
            return difference == 0;
        }
    }
}
