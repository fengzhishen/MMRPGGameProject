//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-16 22:13:54
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    private RectTransform rectTransform = null;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
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