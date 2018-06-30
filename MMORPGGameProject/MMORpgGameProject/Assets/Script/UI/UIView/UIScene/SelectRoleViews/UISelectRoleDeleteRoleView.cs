using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UISelectRoleDeleteRoleView : UIViewBase
{
    /// <summary>
    /// 输入ok框
    /// </summary>
    [SerializeField]
    private InputField m_txtOk;

    /// <summary>
    /// 提示信息
    /// </summary>
    [SerializeField]
    private Text m_TextTip;

    /// <summary>
    /// 移动的目标点
    /// </summary>
    private Vector3 m_moveTargetPos;

    public Action m_OnBtnClick;

    protected override void OnAwake()
    {
        base.OnAwake();
        transform.localPosition = new Vector3(0, 1000, 0);
    }

    protected override void OnStart()
    {
        base.OnStart();
        transform.DOLocalMove(Vector3.zero, 1f).SetAutoKill(false).SetEase<Tweener>(GlobalInit.Instance.UIAnimationCurve).Pause();
    }

    /// <summary>
    /// 显示删除角色窗体
    /// </summary>
    /// <param name="nickName"></param>
    public void Show(string nickName,Action onBtnClick)
    {
        this.m_OnBtnClick = onBtnClick;
        m_TextTip.text = string.Format("你确定要删除<color=#ff0000ff>{0}</color>吗？", nickName);
        transform.DOPlayForward();
    }

    /// <summary>
    /// 关闭删除角色窗体
    /// </summary>
    private void Close()
    {
        transform.DOPlayBackwards();
    }

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        switch (go.name)
        {
            case "btnClose":
                Close();
                break;
            case "btnOK":
                OnBtnClick();

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 确定按钮点击
    /// </summary>
    private void OnBtnClick()
    {
        if(string.IsNullOrEmpty(m_txtOk.text) || m_txtOk.text.Equals("OK",System.StringComparison.InvariantCultureIgnoreCase) == false)
        {
            UIMessageCtr.Instance.Show("提示", "请输入ok删除角色");
        }

        if(m_OnBtnClick !=null)
        {
            m_OnBtnClick();
        }

        Close();
    }
}
