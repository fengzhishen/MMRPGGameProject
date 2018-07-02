using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMainCityRoleInfoView : UIViewBase
{
    /// <summary>
    /// ͷ��
    /// </summary>
    [SerializeField]
    private Image m_imgHeadPic;

    /// <summary>
    /// �ǳ�
    /// </summary>
    [SerializeField]
    private Text m_lbNickName;

    /// <summary>
    /// �ȼ�
    /// </summary>
    [SerializeField]
    private Text m_lbLv;

    /// <summary>
    /// Ԫ��
    /// </summary>
    [SerializeField]
    private Text m_lbMoney;

    /// <summary>
    /// ���
    /// </summary>
    [SerializeField]
    private Text m_lbGold;

    /// <summary>
    /// HP
    /// </summary>
    [SerializeField]
    private Slider m_sliderHP;

    /// <summary>
    /// MP
    /// </summary>
    [SerializeField]
    private Slider m_sliderMP;

    public static UIMainCityRoleInfoView Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetUI(string headPic,string nickName,int level,int money,int gold,int currHP,int currMP,int maxHP,int maxMP)
    {
        this.m_imgHeadPic.sprite = RoleMgr.Instance.LoadRoleHead(headPic);
        this.m_lbNickName.text = nickName;
        this.m_lbLv.text = string.Format("LV.{0}",level.ToString());
        this.m_lbMoney.text = money.ToString();
        this.m_lbGold.text = gold.ToString();
        this.m_sliderHP.value = currHP*1.0F / maxHP;
        this.m_sliderMP.value = currMP * 1.0F / maxMP;
    }
}
