using System.Security.Cryptography;
using System.Text;

namespace BloomAdmin.Main.Expansion
{
    public static class SecurityHelper
    {
        #region MD5 加密（不可逆）

        /// <summary>
        /// MD5 加密（32位小写）
        /// </summary>
        public static string MD5Encrypt(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = MD5.HashData(inputBytes);

            StringBuilder sb = new();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2")); // 转为小写十六进制字符串
            }
            return sb.ToString();
        }

        #endregion

        #region AES 加密解密（推荐）

        /// <summary>
        /// AES 加密（默认使用 AES-256）
        /// </summary>
        public static string AESEncrypt(string plainText, string key, string iv = null)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));

            byte[] keyBytes = new byte[32]; // 默认 256 位
            byte[] keyInput = Encoding.UTF8.GetBytes(key);
            Array.Copy(keyInput, keyBytes, Math.Min(keyInput.Length, keyBytes.Length));

            byte[] ivBytes = iv != null ? Encoding.UTF8.GetBytes(iv) : new byte[16];

            using Aes aes = Aes.Create();
            aes.Key = keyBytes;
            aes.IV = ivBytes;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using ICryptoTransform encryptor = aes.CreateEncryptor();
            byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary>
        /// AES 解密
        /// </summary>
        public static string AESDecrypt(string cipherText, string key, string iv = null)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException(nameof(cipherText));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));

            byte[] keyBytes = new byte[32];
            byte[] keyInput = Encoding.UTF8.GetBytes(key);
            Array.Copy(keyInput, keyBytes, Math.Min(keyInput.Length, keyBytes.Length));

            byte[] ivBytes = iv != null ? Encoding.UTF8.GetBytes(iv) : new byte[16];

            using Aes aes = Aes.Create();
            aes.Key = keyBytes;
            aes.IV = ivBytes;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using ICryptoTransform decryptor = aes.CreateDecryptor();
            byte[] encryptedBytes = Convert.FromBase64String(cipherText);
            byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }

        #endregion

        #region RSA 加密解密（非对称加密）

        private static RSAParameters _publicKey;
        private static RSAParameters _privateKey;

        /// <summary>
        /// 生成 RSA 密钥对（调用一次即可）
        /// </summary>
        public static void GenerateRSAKey()
        {
            using RSACryptoServiceProvider rsa = new();
            _publicKey = rsa.ExportParameters(false);  // 公钥
            _privateKey = rsa.ExportParameters(true); // 私钥
        }

        /// <summary>
        /// RSA 加密（使用公钥）
        /// </summary>
        public static string RSAEncrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) throw new ArgumentNullException(nameof(plainText));

            using RSACryptoServiceProvider rsa = new();
            rsa.ImportParameters(_publicKey);

            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedData = rsa.Encrypt(dataToEncrypt, true); // OAEP 填充
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// RSA 解密（使用私钥）
        /// </summary>
        public static string RSADecrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) throw new ArgumentNullException(nameof(cipherText));

            using RSACryptoServiceProvider rsa = new();
            rsa.ImportParameters(_privateKey);

            byte[] dataToDecrypt = Convert.FromBase64String(cipherText);
            byte[] decryptedData = rsa.Decrypt(dataToDecrypt, true); // OAEP 填充
            return Encoding.UTF8.GetString(decryptedData);
        }

        #endregion
    }
}