using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIMessageView : MonoBehaviour {

    /// <summary>
    /// ����
    /// </summary>
    [SerializeField]
    private Text titleTxt;

    /// <summary>
    /// ����
    /// </summary>
    [SerializeField]
    private Text message;

    /// <summary>
    /// ȡ����ť
    /// </summary>
    [SerializeField]
    private Button btnCancle;

    /// <summary>
    /// ȷ����ť
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
    /// ���ȷ����ť�Ļص�����
    /// </summary>
    private void OnOKBtnClickCallback(GameObject @object)
    {
        if(OnOKBtnClickHandler != null)
        {
            OnOKBtnClickHandler();
        }
    }

    /// <summary>
    /// ���ȡ����ť�ص�����
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
