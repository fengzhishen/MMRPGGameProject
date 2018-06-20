using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System;

/// <summary>
/// 加密工具
/// </summary>
public class EncryptUtil
{

	public static string MD5(string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;

        MD5 d5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] valueHash = d5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value));
        string strResult = BitConverter.ToString(valueHash);

        return strResult.Replace("-","");
    }
}
