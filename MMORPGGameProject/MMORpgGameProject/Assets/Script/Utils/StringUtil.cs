//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-11-16 22:26:09
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// string扩展方法
/// </summary>
public  static class StringUtil 
{
    ///// <summary>
    ///// 扩展方法
    ///// </summary>
    ///// <param name="str"></param>
    public static int ToInt(this string str)
    {
        int temp = 0;
        bool outResult = int.TryParse(str, out temp);
        if(outResult)
        {
            return temp;
        }
        else
        {
            try
            {
                throw new FormatException("数据类型转换失败");
            }
            catch (FormatException e)
            {

                Debug.LogError(e.Message);
            }
            finally
            {
                
            }
            return 0;
        }
    }

    ///// <summary>
    ///// 扩展方法
    ///// </summary>
    ///// <param name="str"></param>
    public static float ToFloat(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            Debug.LogError("字符串非法" + str);
        }
        return float.Parse(str);
    }

    public static long ToLong(this string str)
    {
        try
        {
            return long.Parse(str);
        }
        catch (FormatException e)
        {
            Debug.Log(e.Message);
        }
        catch (ArgumentNullException e)
        {
            Debug.Log(e.Message);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        
        {
            Debug.Log("类型转换失败");
        }
        return long.MinValue;       
    }
}