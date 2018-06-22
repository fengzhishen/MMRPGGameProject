//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-03-12 21:47:40
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System;

public class DelegateDefine : Singleton<DelegateDefine>
{
    /// <summary>
    /// 场景加载完毕
    /// </summary>
    public Action OnSceneLoadOk;
}