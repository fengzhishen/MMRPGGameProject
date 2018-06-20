using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class LanguageSettingsWindow : EditorWindow
{
    private List<string> m_list = new List<string>();
    int valueIndx = 0;
    LanguageSettingsWindow()
    {
        base.titleContent = new GUIContent("语言设置");
        m_list.AddRange(new string[]{ "CN", "EN" });
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal("Box");

        valueIndx = EditorGUILayout.Popup("游戏项目语言", valueIndx, m_list.ToArray());

        EditorGUILayout.Space();

        if(GUILayout.Button("保存"))
        {
            EditorApplication.delayCall = OnSelectLanguageSettingCallback;
        }
        EditorGUILayout.EndHorizontal();

    }

    private void OnSelectLanguageSettingCallback()
    {
        LanguageDBModel.GetInstance.m_currentLanguage = (Language)Enum.Parse(typeof(Language),m_list[valueIndx]);

        youyou_textCom[] youyou_Texts = UnityEngine.Object.FindObjectsOfType<youyou_textCom>();

        for (int i = 0; i < youyou_Texts.Length; i++)
        {
            youyou_Texts[i].Refresh();
        }

        // EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        //当场景中发生变化时候  马上更新
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
    }
}
