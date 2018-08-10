using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLevelCtrl : SystemBaseCtr<GameLevelCtrl>
{
    private UIGameLevelMapView m_uIGameLevelMapView;
    private UIGameLevelDetailView m_levelDetailView;
    private Transform m_windowUiPanelContainerTrans;
    public void OpenView(WindowUIType windowUIType,Transform windowUiPanelContainerTrans)
    {
        m_windowUiPanelContainerTrans = windowUiPanelContainerTrans;
        switch (windowUIType)
        {          
            case WindowUIType.GameLevelMap:
                OpenGameLevelMapView(windowUiPanelContainerTrans);
                break;
            case WindowUIType.GameLevelDetail:
                OpenGameLevelDetailView(windowUiPanelContainerTrans);
                break;
            default:
                break;
        }
    }

    public void OpenGameLevelMapView(Transform windowUiPanelContainerTrans)
    {
        //读取本地配置表
        //默认读取第一章
        ChapterEntity chapterEntity = ChapterDBModel.GetInstance.GetEntityById(1);

        TransferData data = null;
        if (chapterEntity != null)
        {
            data = new TransferData();
            {
                data.SetValue<string>("ChapterName", chapterEntity.ChapterName);
                data.SetValue<int>("ChapterId", chapterEntity.Id);
                data.SetValue<string>("ChapterMapBg", chapterEntity.BG_Pic);
            }
        }

        //读取关卡
        List<GameLevelEntity> gameLevelEntities = GameLevelDBModel.GetInstance.GetListByChapterId(chapterEntity.Id);
        if (gameLevelEntities != null || gameLevelEntities.Count > 0)
        {
            data.SetValue<List<GameLevelEntity>>("GameLevelList", gameLevelEntities);
        }

        m_uIGameLevelMapView = WindowUIMgr.Instance.OpenWindow(WindowUIType.GameLevelMap).GetComponent<UIGameLevelMapView>();
        m_uIGameLevelMapView.transform.SetParent(windowUiPanelContainerTrans);
        m_uIGameLevelMapView.transform.localScale = Vector3.one *0.8f;
        m_uIGameLevelMapView.transform.localPosition = Vector3.zero;
        m_uIGameLevelMapView.transform.localRotation = Quaternion.identity;

        m_uIGameLevelMapView.SetUI(data, OnClickGameLevelCallback, windowUiPanelContainerTrans);
    }

    public void OpenGameLevelDetailView(Transform windowUiPanelContainerTrans)
    {
        m_levelDetailView = WindowUIMgr.Instance.OpenWindow(WindowUIType.GameLevelMap).GetComponent<UIGameLevelDetailView>();
        m_uIGameLevelMapView.transform.SetParent(windowUiPanelContainerTrans);
        m_uIGameLevelMapView.transform.localScale = Vector3.one * 0.8f;
        m_uIGameLevelMapView.transform.localPosition = Vector3.zero;
        m_uIGameLevelMapView.transform.localRotation = Quaternion.identity;
    }

    private void OnClickGameLevelCallback(int gameLevelId,Transform container)
    {
        AppDebug.Log(gameLevelId);
        m_levelDetailView = WindowUIMgr.Instance.OpenWindow(WindowUIType.GameLevelDetail).GetComponent<UIGameLevelDetailView>();
        //GameObject @object = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIWindow, "Pan_GameLevelDetail");
        m_levelDetailView.transform.SetParent(container);
        m_levelDetailView.gameObject.SetGameObjNormalTransfor(0.8f);

        SetGameLevelDetailData(gameLevelId, 0, m_levelDetailView);
    }

    TransferData data = null;
    /// <summary>
    /// 设置游戏关卡详情
    /// </summary>
    /// <param name="gameLevelId"></param>
    /// <param name="grade"></param>
    private void SetGameLevelDetailData(int gameLevelId,GameLevelGrade grade, UIGameLevelDetailView uIGameLevelDetailView)
    {     
        SetGameLevelDetailDes(gameLevelId, grade);
        uIGameLevelDetailView.SetUI(data, SetGameLevelDetailDes);
    }

    private void SetGameLevelDetailDes(int gameLevelId, GameLevelGrade grade)
    {
        //1.读取游戏关卡表
        GameLevelEntity gameLevelEntity = GameLevelDBModel.GetInstance.GetEntityById(gameLevelId);

        //2.读取游戏关卡难度等级表
        GameLevelGradeEntity gameLevelGradeEntity = GameLevelGradeDBModel.GetInstance.GetEntityByGameLevelIdAndGrade(gameLevelId, grade);

        data = new TransferData();
        data.SetValue<string>("GameLevelDesc", gameLevelGradeEntity.Desc);
        data.SetValue<string>("GameLevelConditionDesc", gameLevelGradeEntity.ConditionDesc);
        data.SetValue<string>("GameLevelDlgPic", gameLevelEntity.DlgPic);
        data.SetValue<int>("GameLevelExp", gameLevelGradeEntity.Exp);
        data.SetValue<int>("GameLevelGold", gameLevelGradeEntity.Gold);
        data.SetValue<int>("GameLevelCommentFighting", gameLevelGradeEntity.CommendFighting);
        data.SetValue<string>("GameLevelName", gameLevelEntity.Name);
        data.SetValue<string>("GameLevelName", gameLevelEntity.Name);
        data.SetValue<int>("GameLevelId", gameLevelEntity.Id);

        m_levelDetailView.SetUI(data, SetGameLevelDetailDes);
    }
}
