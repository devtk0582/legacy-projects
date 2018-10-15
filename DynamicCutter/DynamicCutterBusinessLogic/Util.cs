using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace DynamicCutterBusinessLogic
{
    public class Util
    {
        public static string EncryptString(string strInput)
        {
            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
            byte[] bytesValue = System.Text.Encoding.UTF8.GetBytes(strInput);
            byte[] bytesHash = hashAlg.ComputeHash(bytesValue);
            return Convert.ToBase64String(bytesHash);
        }
    }
}
