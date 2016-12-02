﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

// https://stackoverflow.com/questions/8871337/how-can-i-encrypt-user-settings-such-as-passwords-in-my-application#8875545
public static class SecureIt
{
    private static readonly byte[] entropy = Encoding.Unicode.GetBytes("Salt Is Not A Password");

    public static string EncryptString(this SecureString input)
    {
        if (input == null)
        {
            return null;
        }

        var encryptedData = ProtectedData.Protect(
            Encoding.Unicode.GetBytes(input.ToInsecureString()),
            entropy,
            DataProtectionScope.CurrentUser);

        return Convert.ToBase64String(encryptedData);
    }

    public static SecureString DecryptString(this string encryptedData)
    {
        if (encryptedData == null)
        {
            return null;
        }

        try
        {
            var decryptedData = ProtectedData.Unprotect(
                Convert.FromBase64String(encryptedData),
                entropy,
                DataProtectionScope.CurrentUser);

            return Encoding.Unicode.GetString(decryptedData).ToSecureString();
        }
        catch
        {
            return new SecureString();
        }
    }

    public static SecureString ToSecureString(this IEnumerable<char> input)
    {
        if (input == null)
        {
            return null;
        }

        var secure = new SecureString();

        foreach (var c in input)
        {
            secure.AppendChar(c);
        }

        secure.MakeReadOnly();
        return secure;
    }

    public static string ToInsecureString(this SecureString input)
    {
        if (input == null)
        {
            return null;
        }

        var ptr = Marshal.SecureStringToBSTR(input);

        try
        {
            return Marshal.PtrToStringBSTR(ptr);
        }
        finally
        {
            Marshal.ZeroFreeBSTR(ptr);
        }
    }

    public static void ToClipboard(this string input)
    {

        System.Windows.Forms.TextBox tb = new System.Windows.Forms.TextBox();
        tb.Multiline=true;
        tb.Text=input;
        tb.SelectAll();
        tb.Copy();

    }

    //public static string FromClipboard(this string output)
    //{ 

    //    System.Windows.Forms.TextBox tb = new System.Windows.Forms.TextBox();
    //    tb.Multiline=true;
    //    tb.Paste();
    //    return tb.Text;

    //}

}