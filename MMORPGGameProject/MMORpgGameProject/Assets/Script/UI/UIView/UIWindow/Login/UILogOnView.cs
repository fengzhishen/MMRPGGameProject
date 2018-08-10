using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILogOnView : UIWindowViewBase
{
    public InputField txt_AccountInpField;
    public InputField txt_PwdInpField;

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        switch (go.name)
        {
            case "Btn_Logon":
                UIDispatcher.Instance.Dispatch(ConstDefine.UILogOnView_Logon,"登录");
                break;
            case "Btn_Register":
                UIDispatcher.Instance.Dispatch(ConstDefine.UILogOnView_Register, "注册");
                break;
            default:
                break;
        }
    }
}
