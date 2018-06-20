using UnityEngine;
using System.Collections;
using System;

public class UIMessageCtr : Singleton<UIMessageCtr>
{
    /// <summary>
    /// 显示窗口
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <param name="type"></param>
    /// <param name="okAction"></param>
    /// <param name="cancelAction"></param>
   public void Show(string title,string message,MessageViewType type = MessageViewType.Ok,Action okAction = null,Action cancelAction =null)
   {
        GameObject @object= ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIWindow, "Pan_Message", false);
        @object.transform.parent = UISceneCtr.Instance.CurrentUIScene.Container_Center;
        @object.transform.localPosition = Vector3.zero;
        @object.transform.localScale = Vector3.one;

        UIMessageView messageView = @object.GetComponent<UIMessageView>();

        messageView.Show(title, message,type,okAction, cancelAction);
        if(okAction != null)
        {
            okAction();
        }

        if (cancelAction == null ? false:true)
        {
            cancelAction.Invoke();
        }
    }
}
