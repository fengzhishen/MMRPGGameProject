using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class UIGameLevelDetailView : UIWindowViewBase
{
    [SerializeField]
    private Text m_title;

    [SerializeField]
    private Image m_detailImage;

    [SerializeField]
    private Text m_lblgold;

    [SerializeField]
    private Text m_lblExp;

    [SerializeField]
    private Text m_lblDescription;

    [SerializeField]
    private Text m_lblCondition;

    [SerializeField]
    private Text m_lblCommendFighting;

    private int m_gameLevelId;

    public Action<int,GameLevelGrade> m_onClickGameLevelGradeBtnHandler;

    [SerializeField]
    private List<Image> m_gameLevelGradeImageList;

    private Color m_currentSelectGradeColor = Color.green;
    private Color m_oldeGradeColor = Color.yellow;

    protected override void OnBtnClick(GameObject go)
    {
        switch (go.name)
        {
            case "btnNormal":
                OnClickGameLevelGradeBtn(GameLevelGrade.Normal);
                SetCurrentClickGradeBtnColor(GameLevelGrade.Normal);
                break;
            case "btnHard":
                OnClickGameLevelGradeBtn(GameLevelGrade.Hard);
                SetCurrentClickGradeBtnColor(GameLevelGrade.Hard);
                break;
            case "btnHell":
                OnClickGameLevelGradeBtn(GameLevelGrade.Hell);
                SetCurrentClickGradeBtnColor(GameLevelGrade.Hell);
                break;
            default:
                break;
        }
    }

    private void SetCurrentClickGradeBtnColor(GameLevelGrade grade)
    {
        foreach (Image item in m_gameLevelGradeImageList)
        {
            item.color = m_oldeGradeColor;
        }

        switch (grade)
        {
            case GameLevelGrade.Normal:
                m_gameLevelGradeImageList[0].color = m_currentSelectGradeColor;
                break;
            case GameLevelGrade.Hard:
                m_gameLevelGradeImageList[1].color = m_currentSelectGradeColor;
                break;
            case GameLevelGrade.Hell:
                m_gameLevelGradeImageList[2].color = m_currentSelectGradeColor;
                break;
            default:
                break;
        }
    }
    public void OnClickGameLevelGradeBtn(GameLevelGrade grade)
    {
        Debug.Log(grade);
        if(m_onClickGameLevelGradeBtnHandler != null)
        {
            m_onClickGameLevelGradeBtnHandler(m_gameLevelId, grade);
        }
    }
    public void SetUI(TransferData data, Action<int,GameLevelGrade> callback)
    {
        SetCurrentClickGradeBtnColor(GameLevelGrade.Normal);
        if (this.m_onClickGameLevelGradeBtnHandler == null)
        {
            this.m_onClickGameLevelGradeBtnHandler = callback;
        }

        m_title.SetTextValue(data.GetValue<string>("GameLevelName"));
        m_detailImage.sprite = GameUtil.LoadGameLevelDetailIcon(data.GetValue<string>("GameLevelDlgPic"));
        m_lblgold.SetTextValue(data.GetValue<int>("GameLevelGold").ToString());
        m_lblExp.SetTextValue(data.GetValue<int>("GameLevelExp").ToString());
        m_lblDescription.SetTextValue(data.GetValue<string>("GameLevelDesc"));
        m_lblCondition.SetTextValue(data.GetValue<string>("GameLevelConditionDesc"));
        m_lblCommendFighting.SetTextValue(data.GetValue<int>("GameLevelCommentFighting").ToString());
        m_gameLevelId = data.GetValue<int>("GameLevelId");
    }
}
