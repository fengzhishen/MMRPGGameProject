using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class UIMainCityMenuView : MonoBehaviour
{
    public static UIMainCityMenuView Instance;

    /// <summary>
    /// 移动的目标位置
    /// </summary>
    private Vector3 m_MoveTargetPos;

    /// <summary>
    /// 移动的时间
    /// </summary>
    private float m_MoveDuration = 1.0f;

    /// <summary>
    /// 动画正方向播放中
    /// </summary>
    private bool m_bFowardPlaying = false;

    /// <summary>
    /// 当切换菜单动画播放完毕委托
    /// </summary>
    public Action m_OnChangeSuccess;

    /// <summary>
    /// 动画是否播放完毕
    /// </summary>
    private bool m_PlayComplete = true;

    void Awake()
    {
        Instance = this;
        m_MoveTargetPos = transform.position + new Vector3(0, 60, 0);
    }

    void Start()
    {
        m_bFowardPlaying = true;
        transform.DOMove(m_MoveTargetPos, m_MoveDuration).SetAutoKill(false).SetEase(Ease.InExpo).Pause()
           .OnRewind(()=>
            {
                m_PlayComplete = true;
                if (m_OnChangeSuccess != null)
                {
                    m_OnChangeSuccess();
                }
            }).OnComplete(()=>
            {
                m_PlayComplete = true;
                if (m_OnChangeSuccess != null)
                {
                    m_OnChangeSuccess();
                }
            });
    }

    public void ChangeState(Action OnChangeMenu)
    {
        if (m_PlayComplete == false) return;

        m_PlayComplete = false;
        if (m_OnChangeSuccess == null && OnChangeMenu != null)
        {
            this.m_OnChangeSuccess = OnChangeMenu;
        }
        if(m_bFowardPlaying == true)
        {
            transform.DOPlayForward();
        }
        else
        {
            transform.DOPlayBackwards();
        }

        m_bFowardPlaying = !m_bFowardPlaying;
    }
}
