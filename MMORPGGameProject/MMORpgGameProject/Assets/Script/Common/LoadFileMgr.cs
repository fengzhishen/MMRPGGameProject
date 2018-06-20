using UnityEngine;
using System.Collections;
using System.IO;

public class LoadFileMgr : Singleton<LoadFileMgr>
{
#if UNITY_EDITOR
#if UNITY_STANDALONE_WIN
     public readonly string LoacalFilePath = Application.dataPath + "/../AssetBundles/Windows";
#elif UNITY_ANDROID
     public readonly string LoacalFilePath = Application.dataPath + "/../AssetBundles/Android";
#elif UNITY_IPHONE
     public readonly string LoacalFilePath = Application.dataPath + "/../AssetBundles/IOS";
#endif
#elif UNITY_ANDROID
     public readonly string LoacalFilePath = Application.persistentDataPath + "/";
#elif UNITY_IPHONE
     public readonly string LoacalFilePath = Application.persistentDataPath + "/";
#endif

    public byte[] GetBuffer(string path)
    {        
        byte[] buffer = null;
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
        }
        return buffer;
    }
}
