using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TransferData : MonoBehaviour
{
    private Dictionary<string, object> m_putValues = new Dictionary<string, object>();

    public Dictionary<string, object> PutValues
    {
        get
        {
            return m_putValues;
        }
    }

    /// <summary>
    /// ������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetValue<T>(string key,T value)
    {
         PutValues[key] = value;
    }

    /// <summary>
    /// ȡֵ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public T GetValue<T>(string key)
    {
        if(PutValues.ContainsKey(key))
        {
            return (T)PutValues[key];
        }
        else
        {
            return default(T);
        }
    }
}
