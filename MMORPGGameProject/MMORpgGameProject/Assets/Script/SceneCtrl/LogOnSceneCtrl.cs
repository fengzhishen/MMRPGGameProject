using UnityEngine;
using System.Collections;

public class LogOnSceneCtrl : MonoBehaviour {

    GameObject obj;

    void Awake()
    {
        UISceneCtr.Instance.LoadSceneUI(UISceneCtr.SceneUIType.LogOn);
    }	
}