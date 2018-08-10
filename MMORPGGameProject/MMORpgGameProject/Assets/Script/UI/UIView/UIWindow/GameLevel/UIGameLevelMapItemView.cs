using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIGameLevelMapItemView : MonoBehaviour
{
    [SerializeField]
    private Text m_gameLevelItemName;

    [SerializeField]
    private Image m_gameLevelItemICon;

    /// <summary>
    /// 关卡详情挂点
    /// </summary>
    private Transform m_containerType;
    /// <summary>
    /// 关卡id
    /// </summary>
    private int m_gameLevelId;

    public Action<int,Transform> m_OnClickGameLevelItemCallback;

    private void Start()
    {
        GetComponentInChildren<Button>().onClick.AddListener(()=>
        {
            if(m_OnClickGameLevelItemCallback != null)
            {
                m_OnClickGameLevelItemCallback(m_gameLevelId, m_containerType);
            }
        }
      );
    }
    public void SetUI(TransferData data,Action<int,Transform> OnClickGameLevelHandler,Transform containerType)
    {
        m_containerType = containerType;
        m_OnClickGameLevelItemCallback = OnClickGameLevelHandler;
        m_gameLevelItemName.text = data.GetValue<string>("GameLevelItemName");
        m_gameLevelItemICon.sprite = GameUtil.LoadGameLevelIcon(data.GetValue<string>("GameLevelItemIcon"));
        m_gameLevelId = data.GetValue<int>("GameLevelItemId");
    }
}
