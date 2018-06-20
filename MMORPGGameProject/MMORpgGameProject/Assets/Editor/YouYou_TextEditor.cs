using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(youyou_textCom),true)]
public class YouYou_TextEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        youyou_textCom com = (youyou_textCom)base.target;

        int valueIndx = -1, index = 0;

        valueIndx = LanguageDBModel.GetInstance.GetModules().IndexOf(com.Module);

        //Module
        index = EditorGUILayout.Popup("模块", valueIndx, LanguageDBModel.GetInstance.GetModules().ToArray(), new GUILayoutOption[]{ GUILayout.Width(100),GUILayout.MaxWidth(200) });

        if(valueIndx != index)
        {
            com.Module = LanguageDBModel.GetInstance.GetModules()[index];

            EditorUtility.SetDirty(base.target);

            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            com.Refresh();
        }

        //key
        valueIndx = LanguageDBModel.GetInstance.GetKeysByModule(com.Module).IndexOf(com.Key);

        index = EditorGUILayout.Popup("Key", valueIndx, LanguageDBModel.GetInstance.GetKeysByModule(com.Module).ToArray(), new GUILayoutOption[] { GUILayout.Width(100), GUILayout.MaxWidth(200) });

        if(valueIndx != index)
        {
            com.Key = LanguageDBModel.GetInstance.GetKeysByModule(com.Module)[index];

            //通知面板 值改变了
            EditorUtility.SetDirty(target);

            //通知unity 改变了

            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

            com.Refresh();
        }
    }
}
