using UnityEngine;
using System.Collections;

public class UISceneMainCityView : UISceneViewBase
{
    protected override void OnStart()
    {
        base.OnStart();
        if(OnLoadComplete != null)
        {
            OnLoadComplete();
        }
    }

    protected override void OnBtnClick(GameObject go)
    {
        switch (go.name)
        {
            case "btnTopMenu":
                ChangeMenState(go);
                break;
            default:
                break;
        }
    }
    protected override void OnAwake()
    {
        base.OnAwake();

    }
    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy(); 
    }

    /// <summary>
    /// «–ªª≤Àµ•œ‘ æ
    /// </summary>
    /// <param name="object"></param>
    private void ChangeMenState(GameObject @object)
    {
        UIMainCityMenuView.Instance.ChangeState(()=>
        {
            @object.transform.localScale = new Vector3(@object.transform.localScale.x, @object.transform.localScale.y * (-1), @object.transform.localScale.z);
        });
    }
}
