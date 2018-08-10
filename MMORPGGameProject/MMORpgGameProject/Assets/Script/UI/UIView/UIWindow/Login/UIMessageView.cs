using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIMessageView : MonoBehaviour {

    /// <summary>
    /// 标题
    /// </summary>
    [SerializeField]
    private Text titleTxt;

    /// <summary>
    /// 内容
    /// </summary>
    [SerializeField]
    private Text message;

    /// <summary>
    /// 取消按钮
    /// </summary>
    [SerializeField]
    private Button btnCancle;

    /// <summary>
    /// 确定按钮
    /// </summary>
    [SerializeField]
    private Button btnOK;

    public Action OnOKBtnClickHandler;
    public Action OnCancelBtnClickHandler;

    private void Awake()
    {
        EventTriggerListener.Get(this.btnCancle.gameObject).onClick = OnOKBtnClickCallback;
        EventTriggerListener.Get(this.btnCancle.gameObject).onClick = OnCancelBtnClickCallback;
    }

    /// <summary>
    /// 点击确定按钮的回调处理
    /// </summary>
    private void OnOKBtnClickCallback(GameObject @object)
    {
        if(OnOKBtnClickHandler != null)
        {
            OnOKBtnClickHandler();
        }
    }

    /// <summary>
    /// 点击取消按钮回调处理
    /// </summary>
    private void OnCancelBtnClickCallback(GameObject @object)
    {
        if(OnCancelBtnClickHandler != null)
        {
            OnCancelBtnClickHandler();
        }
    }
    public void Show(string title,string message,MessageViewType type= MessageViewType.Ok,Action OnOKBtnClickHandler = null ,Action OnCancelBtnClickHandler = null)
    {
        this.titleTxt.text = title;
        this.message.text = message;
        this.OnOKBtnClickHandler = OnOKBtnClickHandler;
        this.OnCancelBtnClickHandler = OnCancelBtnClickHandler; 

         btnCancle.gameObject.SetActive(type != MessageViewType.Ok);
         btnOK.gameObject.SetActive(type == MessageViewType.Ok);

         if (type == MessageViewType.OkAndCancel) return;
         btnOK.transform.localPosition = new Vector3(0, -36, transform.localPosition.z);
    }
}
