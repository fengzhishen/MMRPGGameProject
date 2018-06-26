//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-11-16 21:55:34
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class TestScene : MonoBehaviour 
{
   
    void Awake()
    {
        Debug.Log("当前脚本执行1");
        GameObject @object = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("GameObject"));
        TestComponent component = @object.GetComponent<TestComponent>();
        component.Init();
        Debug.Log("当前脚本执行完毕2");
    }  
}