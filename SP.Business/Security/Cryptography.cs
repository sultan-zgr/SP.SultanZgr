using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business;


public static class Cryptography  
{
    public static string key = "jbqfIoDDFyoi3RR";
    public static string DecryptData(this string value)
    {
          
        return Decrypt(value);
    }
    public static string EncryptData(this string value)
    { 

        return Encrypt(value);
    }
    private static string Decrypt(string value)
    {
        try
        {
            byte[] data = Convert.FromBase64String(value);
            using (HashAlgorithm algoritm = new MD5CryptoServiceProvider())
            {
                byte[] keys = algoritm.ComputeHash(Encoding.UTF8.GetBytes(key));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider()
                {
                    Key = keys,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return UTF8Encoding.UTF8.GetString(results);
                }
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private static string Encrypt(string value)
    {
        byte[] data = UTF8Encoding.UTF8.GetBytes(value);
        using (HashAlgorithm algoritm = new MD5CryptoServiceProvider())
        {
            byte[] keys = algoritm.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider()
            {
                Key = keys,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            })
            {
                ICryptoTransform transform = tripDes.CreateEncryptor();
                byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                return Convert.ToBase64String(results, 0, results.Length);
            }
        }
    }
}
