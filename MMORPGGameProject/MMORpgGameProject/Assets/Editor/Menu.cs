using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class Menu
{
    [MenuItem("悠游工具/全局设计")]
	public static void Settings()
    {
        SettingsWindow window = EditorWindow.GetWindow<SettingsWindow>();
        window.titleContent = new GUIContent("全局设计");
        window.Show();
    }

    [MenuItem("悠游工具/资源打包")]
    public static void AssetBundleCreate()
    {      
        AssetBundleWindow assetBundleWindow = EditorWindow.GetWindow<AssetBundleWindow>();
        assetBundleWindow.Show();
    }

    [MenuItem("悠游工具/语言设置")]
    public static void LanguageSetting()
    {
        LanguageSettingsWindow languageSettings = EditorWindow.GetWindow<LanguageSettingsWindow>();

        languageSettings.Show();
    }
}
