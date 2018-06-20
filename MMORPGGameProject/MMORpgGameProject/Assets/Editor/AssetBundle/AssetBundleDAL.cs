using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

/// <summary>
/// ab包访问层
/// </summary>
public class AssetBundleDAL
{
    /// <summary>
    /// xml路径
    /// </summary>
    private string m_path;
    /// <summary>
    /// 数据集合
    /// </summary>
    private List<AssetBundleEntity> m_list = null;

    public AssetBundleDAL(string path)
    {
        m_path = path;
        m_list = new List<AssetBundleEntity>();
        m_list.Clear();
        AnalyzeXML(path);
    }

    public List<AssetBundleEntity> GetList
    {
        get
        {
            if (m_list != null && m_list.Count > 0)
            {
                return m_list;
            }

            return null;
        }
    }

    private void AnalyzeXML(string path)
    {
        if(!string.IsNullOrEmpty(path))
        {
            try
            {
                XDocument document = XDocument.Load(path);
                XElement root = document.Root;
                XElement assetBundleNode = root.Element("AssetBundle");
                IEnumerable<XElement> elements = assetBundleNode.Elements();
                int index = 100;
                foreach (XElement item in elements)
                {
                    AssetBundleEntity entity = new AssetBundleEntity();
                    entity.Key = index++.ToString();
                    entity.Name = item.Attribute(XName.Get("Name")).Value;
                    entity.Tag = item.Attribute(XName.Get("Tag")).Value;
                    entity.Version = item.Attribute(XName.Get("Version")).Value.ToInt();
                    entity.Size = item.Attribute(XName.Get("Size")).Value.ToLong();
                    entity.ToPath = item.Attribute(XName.Get("ToPath")).Value;

                    IEnumerable<XElement> pathList = item.Elements("Path");
                    foreach (XElement node in pathList)
                    {
                        entity.PathList.Add(node.Attribute(XName.Get("Value")).Value);                  
                    }
                    m_list.Add(entity);
                    
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        else
        {
            Debug.LogError(GetType() + "/AnalyzeXML()/xml路径非法? path=" + path);
            return;
        }
    }
}
