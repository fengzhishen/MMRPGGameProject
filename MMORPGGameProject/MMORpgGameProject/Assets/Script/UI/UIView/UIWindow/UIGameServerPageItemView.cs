using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIGameServerPageItemView : MonoBehaviour
{
    /// <summary>
    /// 页码
    /// </summary>
    private int m_pageIndx;
    [SerializeField]
    private Text m_gameServerPageName;

    public Action<int> m_OnGameServerPageClick;

    void Start()
    {
        this.gameObject.AddComponent<Button>().onClick.AddListener(OnGameServerPageClickCallback);
    }

    private void OnGameServerPageClickCallback()
    {
        if(m_OnGameServerPageClick != null)
        {
            m_OnGameServerPageClick(m_pageIndx);
        }
    }

    public void SetUI(RetGameServerPageEntity retGameServerPageEntity)
    {
        m_pageIndx = retGameServerPageEntity.PageIndex;
        m_gameServerPageName.text = retGameServerPageEntity.Name;
    }
}
