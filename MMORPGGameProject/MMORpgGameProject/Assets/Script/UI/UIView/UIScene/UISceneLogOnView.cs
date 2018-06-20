using UnityEngine;
using System.Collections;

public class UISceneLogOnView : UISceneViewBase
{
    protected override void OnStart()
    {
        base.OnStart();
        StartCoroutine(OpenLogOnWindow());
    }

    private IEnumerator OpenLogOnWindow()
    {
        yield return new WaitForSeconds(2.0f);
        AccountCtr.Instance.OpenLogOnView();
    }  
}
