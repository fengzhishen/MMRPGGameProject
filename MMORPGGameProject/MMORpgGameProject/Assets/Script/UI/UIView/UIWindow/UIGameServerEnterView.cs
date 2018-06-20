using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIGameServerEnterView : UIWindowViewBase
{
    public Text textDefaultGameServer;

    public void SetUI(string gameServerName)
    {
        if (string.IsNullOrEmpty(gameServerName)) return;
        textDefaultGameServer.text = gameServerName;
    }

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        switch (go.name)
        {
            case "ButtonSelectGameServer":
                UIDispatcher.Instance.Dispatch(ConstDefine.UIGameServerEnterView_btnSelectGameServer);
                break;
            case "BtnEnterGame":
                UIDispatcher.Instance.Dispatch(ConstDefine.UIGameServerEnterView_btnEnterGame);
                break;
            default:
                break;
        }
    }
}
