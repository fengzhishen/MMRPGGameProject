using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIResiterView : UIWindowViewBase
{
    public InputField txt_AccountInpField;
    public InputField txt_PwdInpField;

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        switch (go.name)
        {
            case "Btn_Logon":
                UIDispatcher.Instance.Dispatch(ConstDefine.UIResiterView_Logon, "登录");
                break;
            case "Btn_returnLogon":
                UIDispatcher.Instance.Dispatch(ConstDefine.UIResiterView_ReturnRegister, "注册");
                break;
            default:
                break;
        }
    }
}
