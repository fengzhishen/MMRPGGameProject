using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class UIGameLevelMapView : UIWindowViewBase
{
    /// <summary>
    /// �±���
    /// </summary>
    [SerializeField]
    private RawImage m_mapImage;

    /// <summary>
    /// ������
    /// </summary>
    [SerializeField]
    private Text m_chapterNameText;

    /// <summary>
    /// �½�id
    /// </summary>
    private int m_chapterId;

    private List<UIGameLevelMapItemView> m_uIGameLevelMapItemViews = new List<UIGameLevelMapItemView>();

    //�����½������Ĺؿ���Ϣ
    List<GameLevelEntity> gameLevelEntities;

    private void Start()
    {
        StartCoroutine(SetPoint());      
    }

    private IEnumerator SetPoint()
    {
        yield return new WaitForSeconds(0.2f);
        //�¹ؿ�֮���������ӵ�
        Vector3 startPointPos;
        Vector3 endPointPos;
        for (int i = 0; i < m_uIGameLevelMapItemViews.Count - 1 && i < gameLevelEntities.Count; i++)
        {
            startPointPos = m_uIGameLevelMapItemViews[i].transform.localPosition;
            endPointPos = m_uIGameLevelMapItemViews[i + 1].transform.localPosition;
            SetLinePointInteralGameLevel(startPointPos, endPointPos, gameLevelEntities[i]);
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void SetUI(TransferData data,Action<int,Transform> OnClickGameLevelHandler,Transform container)
    {
        m_uIGameLevelMapItemViews.Clear();
        m_chapterNameText.text = data.GetValue<string>("ChapterName");
        m_chapterId = data.GetValue<int>("ChapterId");      
        m_mapImage.texture = GameObjectUtil.LoadGameLevelMapPic(data.GetValue<string>("ChapterMapBg"));

        //�����½������Ĺؿ���Ϣ
        gameLevelEntities = data.GetValue<List<GameLevelEntity>>("GameLevelList");

        if (gameLevelEntities.Count <= 0) return;
        GameObject gameLevelMapItemObj = null;

        UIGameLevelMapItemView uIGameLevelMapItemView = null;

        foreach (GameLevelEntity item in gameLevelEntities)
        {
            gameLevelMapItemObj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIWindowsChild, "GameLevel/GameLevelMapItem", false);

            uIGameLevelMapItemView = gameLevelMapItemObj.GetComponent<UIGameLevelMapItemView>();

            gameLevelMapItemObj.transform.SetParent(m_mapImage.transform);

            gameLevelMapItemObj.SetGameObjNormalTransfor(0.8f);

            gameLevelMapItemObj.transform.localPosition = item.GetGameLevelPosition;

            TransferData transfer = new TransferData();
            {
                transfer.SetValue<string>("GameLevelItemName", item.Name);
                transfer.SetValue<string>("GameLevelItemIcon", item.Ico);
                transfer.SetValue<int>("GameLevelItemId", item.Id);
            };

            uIGameLevelMapItemView.SetUI(transfer, OnClickGameLevelHandler, container);
            m_uIGameLevelMapItemViews.Add(uIGameLevelMapItemView);
        }
    }

    private void SetLinePointInteralGameLevel(Vector3 startPos,Vector3 endPos, GameLevelEntity gameLevelEntity)
    {
        Vector3 dis = endPos - startPos;
        int count = 10; //�ؿ�֮���ĸ���
        Vector3 pointPos;
        for (int i = 0; i < count; i++)
        {
            pointPos = startPos + i * 1.0f / 10 * dis;
            GameObject @object = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIWindowsChild, @"GameLevel\GameLevelMapPoint");
            @object.transform.SetParent(m_mapImage.transform.GetChild(0));
            @object.SetGameObjNormalTransfor();
            @object.transform.localPosition = pointPos;
            @object.GetComponent<UIGameLevelMapPointView>().SetPoint(gameLevelEntity.isBoss >= 1?true:false);
        }
    }
}
