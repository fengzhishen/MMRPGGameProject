//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-16 22:13:54
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class NPCHeadBarView : MonoBehaviour
{
    /// <summary>
    /// 昵称
    /// </summary>
    [SerializeField]
    private Text lblNickName;

    /// <summary>
    /// NPCHeadBar 跟踪的位置
    /// </summary>
    private Transform m_target;

    [SerializeField]
    private Text m_npcTalkContentText;

    [SerializeField]
    private Image m_npcTalkBgImge;

    private RectTransform rectTransform = null;

    private Tweener m_npcScaleBgtweener;

    private Tweener m_npcRotateBgtweener;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        //开始禁用NPC自言自语的背景图片
        if (m_npcTalkBgImge.gameObject.activeSelf)
        {
            m_npcTalkBgImge.gameObject.SetActive(!m_npcTalkBgImge.gameObject.activeSelf);
        }

        m_npcTalkBgImge.transform.localScale = Vector3.zero;

        m_npcScaleBgtweener = m_npcTalkBgImge.transform.DOScale(Vector3.one, 0.8f).SetAutoKill<Tweener>(false).Pause().
            OnPlay(() =>
            {
                m_npcTalkBgImge.gameObject.SetActive(true);

            }).OnRewind(()=> {
                m_npcTalkBgImge.gameObject.SetActive(false);
            });

        m_npcRotateBgtweener = m_npcTalkBgImge.transform.DORotate(new Vector3(0, 0, Random.Range(-20, 20)), 1f, RotateMode.Fast).SetAutoKill(false).Pause().SetLoops(-1,LoopType.Yoyo);
    }

    void Update()
    {
        if (m_target == null) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(m_target.position);

        Vector3 worldPoint = Vector3.zero;

        bool bSuccess = RectTransformUtility.ScreenPointToWorldPointInRectangle(UISceneCtr.Instance.CurrentUIScene.m_Canvas.GetComponent<RectTransform>(), screenPos, UI_Camera.Instance.Camera, out worldPoint);

        if (bSuccess == true)
        {
            transform.position = worldPoint;
        }

        if(Time.time > m_npcTalkStopTime)
        {
            m_npcScaleBgtweener.PlayBackwards();
        }
    }

    /// <summary>
    /// NPC说话停止时间
    /// </summary>
    private float m_npcTalkStopTime;

    /// <summary>
    /// NPC自言自语
    /// </summary>
    /// <param name="npcTalkContent"></param>
    /// <param name="time"></param>
    public void NPCTalck(string npcTalkContent,float time)
    {
        m_npcTalkStopTime = Time.time + time;

        m_npcTalkContentText.text = npcTalkContent;

        m_npcScaleBgtweener.PlayForward();

        m_npcRotateBgtweener.PlayForward();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="nickName"></param>
    public void Init(Transform @object, string nickName)
    {
        m_target = @object;

        lblNickName.text = nickName;
    }

    ///// <summary>
    ///// 上弹伤害文字
    ///// </summary>
    ///// <param name="hurtValue"></param>
    //public void Hurt(int hurtValue, float pbHPValue = 0)
    //{
    //    hudText.Add(string.Format("-{0}", hurtValue), Color.red, 0.1f);
    //    pbHP.value = pbHPValue;
    //}
}