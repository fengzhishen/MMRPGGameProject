using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class UIGameServerSelectView : UIWindowViewBase
{
    /// <summary>
    /// 页签预设
    /// </summary>
    [SerializeField]
    private GameObject m_gameServerPageItemPrefab;

    /// <summary>
    /// 页签列表
    /// </summary>
    [SerializeField]
    private GridLayoutGroup m_gameServerPageGrid;

    protected override void OnAwake()
    {
        base.OnAwake(); 

    }

    /// <summary>
    /// 点击页签之后的回调处理
    /// </summary>
    public Action<int> m_OnGameServerPageClickCallback;

    public void SetGameServerPageUI(IList<RetGameServerPageEntity> list)
    {
        if (list == null || list.Count <= 0) return;

        for (int i = 0; i < list.Count; i++)
        {
            GameObject @object = Instantiate<GameObject>(m_gameServerPageItemPrefab);
            @object.transform.parent = m_gameServerPageGrid.transform;
            @object.transform.localPosition = Vector3.zero;
            @object.transform.localScale = Vector3.one;

            UIGameServerPageItemView uIGameServerPageItemView = @object.GetComponent<UIGameServerPageItemView>();
            if(uIGameServerPageItemView != null)
            {
                uIGameServerPageItemView.SetUI(list[i]);
                uIGameServerPageItemView.m_OnGameServerPageClick = OnGameServerPageClickCallback;
            }
        }
    }

    /// <summary>
    /// 点击服务器页签的回调处理
    /// </summary>
    /// <param name="obj"></param>
    private void OnGameServerPageClickCallback(int pageIndex)
    {
        if(m_OnGameServerPageClickCallback != null)
        {
            m_OnGameServerPageClickCallback(pageIndex);
        }     
    }


    //区服列表相关----------------------------------------------------------------------------------------------------
   
    /// <summary>
    /// 服务器预设
    /// </summary>
    [SerializeField]
    private GameObject m_gameServerItemPrefab;

    /// <summary>
    /// 服务器列表
    /// </summary>
    [SerializeField]
    private GridLayoutGroup m_gameServerGrid;

    private List<GameObject> m_objList = new List<GameObject>();
    /// <summary>
    /// 更新区服列表
    /// </summary>
    /// <param name="entities"></param>
    public void SetGameServerUI(List<RetGameServerEntity> entities, System.Action<string> onClickServerItemHanlder)
    {

        if (entities == null || entities.Count <= 0) return;
        if(m_objList.Count > 0)
        {
            for (int i = 0; i < m_objList.Count; i++)
            {
                DestroyImmediate(m_objList[i]);
            }      
        }

        for (int i = 0; i < entities.Count; i++)
        {
            GameObject @object = Instantiate<GameObject>(m_gameServerItemPrefab);
            m_objList.Add(@object);
            @object.transform.parent = m_gameServerGrid.transform;
            @object.transform.localPosition = Vector3.zero;
            @object.transform.localScale = Vector3.one;

            UIGameServerItemView uIGameServerItemView = @object.GetComponent<UIGameServerItemView>();
            uIGameServerItemView.m_onClickGameServerItemHanlder = onClickServerItemHanlder;

            if (uIGameServerItemView != null)
            {
                uIGameServerItemView.SetUI(entities[i]);
            }
        }
    }
}
