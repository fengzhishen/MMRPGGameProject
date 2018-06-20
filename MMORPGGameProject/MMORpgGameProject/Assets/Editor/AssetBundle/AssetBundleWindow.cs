using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;

public class AssetBundleWindow : EditorWindow
{
    private string[] arrayTag = { "All", "Scene", "Role", "Effect", "Audio", "None" };
    private string[] arrayBuildTarget = { "Windows", "Android", "IOS" };

    private int m_tagIndex = 0;
    private List<AssetBundleEntity> entities = new List<AssetBundleEntity>();
    private Dictionary<string, bool> dic = new Dictionary<string, bool>();

#if UNITY_STANDALONE_WIN
    private BuildTarget target = BuildTarget.StandaloneWindows;
    private int m_buildIndexTarget = 0;
#elif UNITY_ANDROID
    private BuildTarget target = BuildTarget.Android;
    private int m_buildIndexTarget = 1;
#elif UNITY_IPHONE
    private BuildTarget target = BuildTarget.iOS;
    private int m_buildIndexTarget = 2;
#endif

    public AssetBundleWindow()
    {
        string title = "AB资源打包窗体";
        this.titleContent = new GUIContent(title);
        string xmlPath = Application.dataPath + "/Editor/AssetBundle/AssetBundleConfig.xml";
        AssetBundleDAL assetBundleDAL = new AssetBundleDAL(xmlPath);
        entities = assetBundleDAL.GetList;
        for (int i = 0; i < entities.Count; i++)
        {
            dic.Add(entities[i].Key, true);
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal("Box");
        m_tagIndex = EditorGUILayout.Popup(m_tagIndex, arrayTag,GUILayout.Width(80));

        if(GUILayout.Button("选择Tag",GUILayout.Width(60)))
        {
            EditorApplication.delayCall = OnSelectTagCallback;
        }

        m_buildIndexTarget = EditorGUILayout.Popup(m_buildIndexTarget, arrayBuildTarget, GUILayout.Width(80));
        if (GUILayout.Button("选择Target", GUILayout.Width(100)))
        {
            EditorApplication.delayCall = OnSelectTargetCallback;
        }

        if (GUILayout.Button("打AB包", GUILayout.Width(100)))
        {
            EditorApplication.delayCall = OnAssetBundleCallback;
        }
        if (GUILayout.Button("清空AB包", GUILayout.Width(100)))
        {
            EditorApplication.delayCall = OnClearAssetBundleCallback;
        }
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(2);
        EditorGUILayout.BeginHorizontal("Box");
        GUILayout.Label("包名",GUILayout.Width(300));
        GUILayout.Label("标记", GUILayout.Width(80));
        GUILayout.Label("保存路径", GUILayout.Width(80));
        GUILayout.Label("版本", GUILayout.Width(80));
        GUILayout.Label("大小", GUILayout.Width(80));     
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);
        for (int i = 0; i < entities.Count; i++)
        {
            EditorGUILayout.BeginHorizontal("Box");
            dic[entities[i].Key] = GUILayout.Toggle(dic[entities[i].Key], "",GUILayout.Width(10));
            GUILayout.Label(entities[i].Name, GUILayout.Width(290));
            GUILayout.Label(entities[i].Tag, GUILayout.Width(80));
            GUILayout.Label(entities[i].ToPath, GUILayout.Width(80));
            GUILayout.Label(entities[i].Version.ToString(), GUILayout.Width(80));
            GUILayout.Label(entities[i].Size.ToString(), GUILayout.Width(80));         
            EditorGUILayout.EndHorizontal();

            for (int j = 0; j < entities[i].PathList.Count; j++)
            {
                GUILayout.Label(entities[i].PathList[j]);
               
            }
            GUILayout.Space(2);
        }
       
    }

    private void OnClearAssetBundleCallback()
    {
        string abPath = Application.dataPath + "/../Assetbundles/" + arrayBuildTarget[m_buildIndexTarget];
        if (Directory.CreateDirectory(abPath).GetFileSystemInfos().Length <= 0) return;
        if (Directory.Exists(abPath))
        {
            Directory.Delete(abPath, true);
        }
        Debug.Log("清空完毕");
    }

    private void OnAssetBundleCallback()
    {
        List<AssetBundleEntity> CanBuildAsset = new List<AssetBundleEntity>();
        for (int i = 0; i < entities.Count; i++)
        {
            if(dic[entities[i].Key])
            {
                CanBuildAsset.Add(entities[i]);
            }
        }
        for (int i = 0; i < CanBuildAsset.Count; i++)
        {
            BuildAssetBundle(entities[i]);
        }
    }

    private void BuildAssetBundle(AssetBundleEntity entity)
    {
        AssetBundleBuild[] builds = new AssetBundleBuild[1];
        //包名
        Debug.Log(entity.Name + entity.Tag);
        builds[0].assetBundleName = entity.Name; //string.Format("{0}.{1}", entity.Name, entity.Tag.Equals("Scene",StringComparison.CurrentCultureIgnoreCase) ? "Unity3d" : "assetBundle");
        builds[0].assetBundleVariant = entity.Tag.Equals("Scene", StringComparison.CurrentCultureIgnoreCase) ? "Unity3d" : "assetBundle";
        builds[0].assetNames = entity.PathList.ToArray();      
        string abOutDir = Application.dataPath + "/../Assetbundles/" + arrayBuildTarget[m_buildIndexTarget] + "/" + entity.ToPath;
        if(!Directory.Exists(abOutDir))
        {
            Directory.CreateDirectory(abOutDir);
        }

        BuildPipeline.BuildAssetBundles(abOutDir, builds, BuildAssetBundleOptions.None,target);
       
    }
    private void OnSelectTargetCallback()
    {
        switch (m_buildIndexTarget)
        {
            case 0:
                target = BuildTarget.StandaloneWindows;
                break;
            case 1:
                target = BuildTarget.Android;
                break;
            case 2:
                target = BuildTarget.iOS;
                break;
            default:
                break;
        }
        Debug.Log(target);
    }

    private void OnSelectTagCallback()
    {
        switch (m_tagIndex)
        {
            case 0:
                for (int i = 0; i < dic.Count; i++)
                {
                    dic[entities[i].Key] = true;
                }
                break;
            case 1:
                for (int i = 0; i < dic.Count; i++)
                {
                    dic[entities[i].Key] =  object.Equals(entities[i].Tag, "Scene");
                }
                break;
            case 2:
                for (int i = 0; i < dic.Count; i++)
                {
                    dic[entities[i].Key] = object.Equals(entities[i].Tag, "Role");
                }
                break;
            case 3:
                for (int i = 0; i < dic.Count; i++)
                {
                    dic[entities[i].Key] = object.Equals(entities[i].Tag, "Effect");
                }
                break;
            case 4:
                for (int i = 0; i < dic.Count; i++)
                {
                    dic[entities[i].Key] = object.Equals(entities[i].Tag, "Audio");
                }
                break;
            case 5:
                for (int i = 0; i < dic.Count; i++)
                {
                    dic[entities[i].Key] = object.Equals(entities[i].Tag, "None");
                }
                break;
            default:
                break;
        }
    }
}
