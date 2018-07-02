using UnityEngine;
using System.Collections;
using System;

public class UISceneViewBase : UIViewBase
{
    /// <summary>
    /// ÈÝÆ÷_¾ÓÖÐ
    /// </summary>
    [SerializeField]
    public Transform Container_Center;

    public Action OnLoadComplete;
}
