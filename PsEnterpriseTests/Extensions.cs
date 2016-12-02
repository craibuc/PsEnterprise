using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace PsEnterpriseTests
{
    /**
     * http://stackoverflow.com/a/38016279/134367
     * http://stackoverflow.com/a/8874634/134367
     * */
    public static class Extensions
    {

        //static byte[] entropy = System.Text.Encoding.Unicode.GetBytes("password salt");
        //static byte[] entropy;

        /// <summary>
        /// convert a secure string into a normal plain text string
        /// </summary>
        /// <param name="secureStr"></param>
        /// <returns>String</returns>
        public static String ToPlainString(this System.Security.SecureString secureStr)
        {
            String plainStr = new System.Net.NetworkCredential(string.Empty, secureStr).Password;
            return plainStr;
        }

        /// <summary>
        /// convert a plain text string into a secure string
        /// </summary>
        /// <param name="plainStr"></param>
        /// <returns>System.Security.SecureString</returns>
        public static System.Security.SecureString ToSecureString(this String plainStr)
        {
            var secStr = new System.Security.SecureString(); secStr.Clear();
            foreach (char c in plainStr.ToCharArray())
            {
                secStr.AppendChar(c);
            }
            return secStr;
        }

        //public static System.Security.SecureString DecryptString(this string encryptedData)
        //{
        //    if (encryptedData == null)
        //    {
        //        return null;
        //    }

        //    try
        //    {
        //        var decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
        //            Convert.FromBase64String(encryptedData),
        //            null,
        //            DataProtectionScope.CurrentUser);

        //        return Encoding.Unicode.GetString(decryptedData).ToSecureString();
        //    }
        //    catch
        //    {
        //        return new SecureString();
        //    }
        //}

    } // class
} // namespace
