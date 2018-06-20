//===================================================
//��    �ߣ�����  http://www.u3dol.com  QQȺ��87481002
//����ʱ�䣺2015-11-30 22:05:46
//��    ע��
//===================================================
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

/// <summary>
/// ����UI��ͼ�Ļ���
/// </summary>
public class UIViewBase : MonoBehaviour
{
    void Start()
    {
        Button[] btnArr = GetComponentsInChildren<Button>(true);
        for (int i = 0; i < btnArr.Length; i++)
        {
            EventTriggerListener.Get(btnArr[i].gameObject).onClick = BtnClick;
        }
        OnStart();
    }

    void OnDestroy()
    {
        BeforeOnDestroy();
    }

    private void BtnClick(GameObject go)
    {
        OnBtnClick(go);
    }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void BeforeOnDestroy() { }
    protected virtual void OnBtnClick(GameObject go) { }
}