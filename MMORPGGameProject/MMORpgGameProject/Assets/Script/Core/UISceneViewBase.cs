using UnityEngine;
using System.Collections;
using System;

public class UISceneViewBase : UIViewBase
{
    /// <summary>
    /// ����_����
    /// </summary>
    [SerializeField]
    public Transform Container_Center;

    public Action OnLoadComplete;
}
