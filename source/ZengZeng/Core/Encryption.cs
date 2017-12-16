using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ZengZeng.Core
{
    public class Cipher
    {
        const string a_strKey = "#s^un2ye31<cn%|boXpR,+vu";
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="a_strString"></param>
        /// <param name="a_strKey"></param>
        /// <returns></returns>
        public static string Encrypt3DES(string a_strString)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Key = ASCIIEncoding.ASCII.GetBytes(a_strKey);
            DES.Mode = CipherMode.ECB;

            ICryptoTransform DESEncrypt = DES.CreateEncryptor();

            byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(a_strString);
            byte[] bytes = DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length);
            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="a_strString"></param>
        /// <param name="a_strKey"></param>
        /// <returns></returns>
        public static string Decrypt3DES(string a_strString)
        {
            if (string.IsNullOrEmpty(a_strString))
            {
                return "";
            }
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Key = ASCIIEncoding.ASCII.GetBytes(a_strKey);
            DES.Mode = CipherMode.ECB;
            DES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            ICryptoTransform DESDecrypt = DES.CreateDecryptor();

            string result = "";
            try
            {
                byte[] Buffer = Convert.FromBase64String(a_strString);
                result = ASCIIEncoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception e)
            {

            }
            return result;
        }
    }
}