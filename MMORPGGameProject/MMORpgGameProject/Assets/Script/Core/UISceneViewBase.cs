using UnityEngine;
using System.Collections;
using System;

public class UISceneViewBase : UIViewBase
{
    public Canvas m_Canvas;
    /// <summary>
    /// ÈÝÆ÷_¾ÓÖÐ
    /// </summary>
    [SerializeField]
    public Transform Container_Center;

    public Action OnLoadComplete;
}
