using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class SettingsWindow : EditorWindow
{
    private IList<MacroItem> m_list = new List<MacroItem>();
    private Dictionary<string, bool> m_dic = new Dictionary<string, bool>();
    private string MacroStr = string.Empty;

    public SettingsWindow()
    {
        MacroStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        m_list.Add(new MacroItem() { Name = "DEBUG_MODEL", DisplayName = "调试模式", IsDebug = true, IsRelease = false });

        m_list.Add(new MacroItem() { Name = "DEBUG_LOG", DisplayName = "打印日志模式", IsDebug = true, IsRelease = false });

        m_list.Add(new MacroItem() { Name = "STAT_TD", DisplayName = "开启统计", IsDebug = false, IsRelease = true });

        for (int i = 0; i < m_list.Count; i++)
        {
            m_dic.Add(m_list[i].Name, false);
        }
    }

    public void OnGUI()
    {
        for (int i = 0; i < m_list.Count; i++)
        {
            EditorGUILayout.BeginHorizontal("Box");
            m_dic[m_list[i].Name] = GUILayout.Toggle(m_dic[m_list[i].Name], m_list[i].DisplayName);          
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("保存宏定义", GUILayout.Width(60), GUILayout.ExpandWidth(true)))
        {
            SaveMacro();
        }

        if(GUILayout.Button("调试模式",GUILayout.Width(60),GUILayout.ExpandWidth(true)))
        {
            for (int i = 0; i < m_list.Count; i++)
            {
                m_dic[m_list[i].Name] = m_list[i].IsDebug;
            }
        }

        if (GUILayout.Button("发布模式",GUILayout.Width(60), GUILayout.ExpandWidth(true)))
        {
            for (int i = 0; i < m_list.Count; i++)
            {
                m_dic[m_list[i].Name] = m_list[i].IsRelease;
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    private void SaveMacro()
    {
        MacroStr = string.Empty;
        foreach (var item in m_dic)
        {
            if(item.Value == true)
            {
                MacroStr += string.Format("{0};",item.Key);
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS| BuildTargetGroup.Android | BuildTargetGroup.Standalone, MacroStr);
            }
        }
    }
    /// <summary>
    /// 宏
    /// </summary>
    public class MacroItem
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 显示的名称
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// 是否调试项
        /// </summary>
        public bool IsDebug;

        /// <summary>
        /// 是否发布项
        /// </summary>
        public bool IsRelease;
    }
}
