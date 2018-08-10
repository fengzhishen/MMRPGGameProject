using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIRoleInfoDetailView : MonoBehaviour
{
    [SerializeField]
    private Text m_lblMoney;

    [SerializeField]
    private Text m_lblGold;

    [SerializeField]
    private Text m_lblHP;

    [SerializeField]
    private Slider m_sliderHP;

    [SerializeField]
    private Text m_lblMP;

    [SerializeField]
    private Slider m_silderMP;

    [SerializeField]
    private Text m_lblEXP;

    [SerializeField]
    private Slider m_sliderEXP;

    [SerializeField]
    private Text m_lblAttack;

    [SerializeField]
    private Text m_lblDefense;

    [SerializeField]
    private Text m_lblDodge;

    [SerializeField]
    private Text m_lblHit;

    [SerializeField]
    private Text m_lblCri;

    [SerializeField]
    private Text m_lblRes;

    public void SetUI(TransferData data)
    {
        m_lblMoney.SetTextValue((data.GetValue<int>(ConstDefine.Money)).ToString());
        m_lblGold.SetTextValue((data.GetValue<int>(ConstDefine.Gold)).ToString());
        m_lblHP.SetTextValue((data.GetValue<int>(ConstDefine.CurrHP) + "/" + data.GetValue<int>(ConstDefine.MaxHP)));
        m_sliderHP.SetSliderValue((data.GetValue<int>(ConstDefine.CurrHP)*1.0F/(data.GetValue<int>(ConstDefine.MaxHP))*1.0F));
        m_lblMP.SetTextValue((data.GetValue<int >(ConstDefine.CurrMP) + "/" + data.GetValue<int>(ConstDefine.MaxMP)));
        m_silderMP.SetSliderValue((data.GetValue<int>(ConstDefine.CurrMP)*1.0f/(data.GetValue<int>(ConstDefine.MaxMP)*1.0f)));
        m_lblEXP.SetTextValue((data.GetValue<int>(ConstDefine.Exp) + "/" + data.GetValue<int>(ConstDefine.Exp)));
        m_sliderEXP.SetSliderValue((data.GetValue<int>(ConstDefine.Exp)*1.0f /(1.0f*data.GetValue<int>(ConstDefine.Exp))));
        m_lblAttack.SetTextValue((data.GetValue<int>(ConstDefine.Attack)).ToString());
        m_lblDefense.SetTextValue((data.GetValue<int>(ConstDefine.Defense)).ToString());
        m_lblDodge.SetTextValue((data.GetValue<int>(ConstDefine.Dodge)).ToString());
        m_lblHit.SetTextValue((data.GetValue<int>(ConstDefine.Hit)).ToString());
        m_lblCri.SetTextValue((data.GetValue<int>(ConstDefine.Cri)).ToString());
        m_lblRes.SetTextValue((data.GetValue<int>(ConstDefine.Res)).ToString());
    }
}
