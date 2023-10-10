using System;
using System.IO;
using System.Security.Cryptography;


namespace AES
{
    class AES_Code
    {
        public byte[] EncryptStringToByte(string plainText, byte[] key, byte[] IV)
        {
            if(plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (IV == null || IV.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = IV;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using(MemoryStream msen = new MemoryStream())
                {
                    using(CryptoStream scen = new CryptoStream(msen, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swen = new StreamWriter(scen)) swen.Write(plainText);
                    }
                    encrypted = msen.ToArray();
                }
            }
            return encrypted;
        }
        public string DecryptStringFromByte(byte[] clipperText, byte[] key, byte[] IV)
        {
            if (clipperText == null || clipperText.Length <= 0)
            {
                throw new ArgumentNullException("clipperText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (IV == null || IV.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            string plainText = null;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = IV;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using(MemoryStream decr = new MemoryStream(clipperText))
                {
                    using(CryptoStream cscr = new CryptoStream(decr,decryptor, CryptoStreamMode.Read))
                    {
                        using(StreamReader srde = new StreamReader(cscr))
                        {
                            plainText = srde.ReadToEnd();
                        }
                    }
                }
            }
            return plainText;
        }
    }
}
