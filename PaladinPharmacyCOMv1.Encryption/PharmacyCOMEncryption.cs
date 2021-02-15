using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PaladinPharmacyCOMv1.Encryption
{
    class PharmacyCOMEncryption
    {
        public PharmacyCOMEncryption(string pwd)
        {
            var hash = MD5.Create();
            this.key = hash.ComputeHash(Encoding.Unicode.GetBytes(pwd ?? string.Empty));
        }

        private byte[] key;

        public string Encrypt(string msg)
        {
            try
            {
                var aes = Aes.Create();
                aes.Key = key;
                aes.GenerateIV();

                var bytes = Encoding.Unicode.GetBytes(msg);
                byte[] encrypted;

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytes, 0, bytes.Length);
                    }
                    encrypted = ms.ToArray();
                }

                //return iv prepended to data
                var totalBytes = new byte[encrypted.Length + aes.IV.Length];
                Buffer.BlockCopy(aes.IV, 0, totalBytes, 0, aes.IV.Length);
                Buffer.BlockCopy(encrypted, 0, totalBytes, aes.IV.Length, encrypted.Length);
                return Convert.ToBase64String(totalBytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        public string Decrypt(string strEncrypted)
        {
            try
            {
                var encrypted = Convert.FromBase64String(strEncrypted);

                var aes = Aes.Create();
                aes.Key = key;

                //extract iv from byte array
                aes.IV = encrypted.Take(16).ToArray();
                encrypted = encrypted.Skip(16).ToArray();

                var decrypted = new byte[encrypted.Length];

                using (var ms = new MemoryStream(encrypted))
                {
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        cs.Read(decrypted, 0, decrypted.Length);
                    }
                }

                return Encoding.Unicode.GetString(decrypted);
            }
            catch
            {
                return string.Empty;
            }

        }
    }
}
