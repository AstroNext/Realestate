using System;
using System.Security.Cryptography;
using System.Text;

namespace RealEstate
{
    class EncriptPassword
    {
        public string secretPhase()
        {
            return "E6V+QQkHXx5OKWoRDulFSA==";
        }
        public string encrypt(string str)
        {
            using (MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding uTF8 = new UTF8Encoding();
                byte[] data = mD5.ComputeHash(uTF8.GetBytes(str));
                return Convert.ToBase64String(data);
            }
        }
    }
}
