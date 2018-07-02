//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-16 22:13:54
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoleHeadBarView : MonoBehaviour
{
    /// <summary>
    /// 昵称
    /// </summary>
    [SerializeField]
    private Text lblNickName;

    /// <summary>
    /// 对齐的目标点
    /// </summary>
    private Transform m_Target;

    private RectTransform rectTransform = null;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (m_Target == null) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(m_Target.position);

        Vector3 worldPoint = Vector3.zero;

        bool bSuccess = RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, screenPos, UI_Camera.Instance.Camera, out worldPoint);

        if(bSuccess == true)
        {
            rectTransform.position = worldPoint;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <param name="nickName"></param>
    /// <param name="isShowHPBar">是否显示血条</param>
    public void Init(Transform target, string nickName, bool isShowHPBar = false)
    {
        m_Target = target;
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