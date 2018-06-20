using UnityEngine;
using System.Collections;
using System;

public class SystemBaseCtr<T> : Singleton<T> where T:new()
{
    protected void ShowMessage(string title, string message, MessageViewType type = MessageViewType.Ok, Action okAction = null, Action cancelAction = null)
    {
        UIMessageCtr.Instance.Show(title, message, type, okAction, cancelAction);
    }

    protected void AddEventListener(string key,  DispatcherBase<UIDispatcher,object,string>.OnActionHandler handler)
    {
        UIDispatcher.Instance.AddEventListener(key, handler);
    }
 
    protected void RemoveEventListener(string key, DispatcherBase<UIDispatcher, object, string>.OnActionHandler handler)
    {
        UIDispatcher.Instance.RemoveEventListener(key, handler);
    }

    protected void Log(object message)
    {
        AppDebug.Log(message);
    }

    protected void LogError(object message)
    {
        AppDebug.LogError(message);
    }

    protected void OpenView(WindowUIType type)
    {
        WindowUIMgr.Instance.OpenWindow(type);
    }
}
