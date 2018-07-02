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
    protected override void OnAwake()
    {
        base.OnAwake();

    }
    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy(); 
    }
}
