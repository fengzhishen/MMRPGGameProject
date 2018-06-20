using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[ExecuteInEditMode]
public class youyou_textCom : MonoBehaviour
{
    /// <summary>
    /// 模块
    /// </summary>
    public string Module;
   
    /// <summary>
    ///key
    /// </summary>
    public string Key;

    public void Refresh()
    {
        if (string.IsNullOrEmpty(Module) || string.IsNullOrEmpty(Key)) return;

        Text text = GetComponent<Text>();
        text.text = LanguageDBModel.GetInstance.GetText(Module, Key);
    }
}
