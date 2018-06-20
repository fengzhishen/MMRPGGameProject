using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// AssetBundle实体
/// </summary>
public class AssetBundleEntity
{
    /// <summary>
    /// 打包时候选的唯一key
    /// </summary>
    public string Key;
    /// <summary>
    /// 名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 标志
    /// </summary>
    public string Tag;

    /// <summary>
    /// 版本号
    /// </summary>
    public int Version;

    /// <summary>
    /// 大小(k)
    /// </summary>
    public long Size;

    /// <summary>
    /// 打包保存的路径
    /// </summary>
    public string ToPath;

    private List<string> m_pathList = new List<string>();

    public List<string> PathList
    {
        get { return m_pathList; }
    }
}
