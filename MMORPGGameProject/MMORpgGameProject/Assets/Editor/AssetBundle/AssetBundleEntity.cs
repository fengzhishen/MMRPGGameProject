using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// AssetBundleʵ��
/// </summary>
public class AssetBundleEntity
{
    /// <summary>
    /// ���ʱ��ѡ��Ψһkey
    /// </summary>
    public string Key;
    /// <summary>
    /// ����
    /// </summary>
    public string Name;

    /// <summary>
    /// ��־
    /// </summary>
    public string Tag;

    /// <summary>
    /// �汾��
    /// </summary>
    public int Version;

    /// <summary>
    /// ��С(k)
    /// </summary>
    public long Size;

    /// <summary>
    /// ��������·��
    /// </summary>
    public string ToPath;

    private List<string> m_pathList = new List<string>();

    public List<string> PathList
    {
        get { return m_pathList; }
    }
}
