using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DispatcherBase<T,P,X>: IDisposable 
    where T:new()

 {
    private static T m_instance;
    public static T Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = new T();
            }
            return m_instance;
        }
    }


    public delegate void OnActionHandler(params P[] p);

    private IDictionary<X, List<OnActionHandler>> dic = new Dictionary<X, List<OnActionHandler>>();

    public void AddEventListener(X key, OnActionHandler handler)
    {
        if (dic.ContainsKey(key))
        {
            dic[key].Add(handler);

        }

        List<OnActionHandler> handlers = new List<OnActionHandler>();
        dic[key] = handlers;
        handlers.Add(handler);
    }

    public void RemoveEventListener(X key, OnActionHandler handler)
    {
        if (dic.ContainsKey(key))
        {
            dic[key].Remove(handler);
            if (dic.Count <= 0)
            {
                dic.Remove(key);
            }
        }
    }

    /// <summary>
    /// 分发消息
    /// </summary>
    /// <param name="key">消息</param>
    /// <param name="param">消息参数</param>
    public void Dispatch(X key, params P[] p)
    {
        if (dic.ContainsKey(key))
        {
            if (dic[key].Count > 0)
            {
                foreach (OnActionHandler handler in dic[key])
                {
                    handler(p);
                }
            }
        }
    }


    public virtual void Dispose()
    {
       
    }
}
