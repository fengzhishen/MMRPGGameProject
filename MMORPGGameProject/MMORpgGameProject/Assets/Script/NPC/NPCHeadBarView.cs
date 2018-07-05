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
    /// 
    /// </summary>
    private Transform m_target;
    void Start()
    {
       
    }

    void Update()
    {
       
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nickName"></param>
    public void Init(Transform @object, string nickName)
    {
        transform.position = @object.position;

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