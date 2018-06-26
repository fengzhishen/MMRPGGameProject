using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 数据操作类的基类
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="P"></typeparam>
public abstract class AbstractDBModel<T,P> where T:new() where P:AbstractEntity,new()
{
    protected List<P> m_dataList;
    private Dictionary<int, P> m_dataDic;
    protected AbstractDBModel()
    {
        m_dataList = new List<P>();
        m_dataList.Clear();
        m_dataDic = new Dictionary<int, P>();
        Load();
    }
    private static T m_instance;
    public static T GetInstance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new T();
            }
            return m_instance;
        }
    }

    //把文件数据提取到实体对象中
    protected abstract P MakeEntity(GameDataTableParser dataParser);

    protected virtual string FileName
    {
        get;set;
    }
   

    protected virtual string DataFileName()
    {
        return @"E:\UnityProject\MMRPGGameProject\MMORPGGameProject\MMORpgGameProject\WWW\Data\" + FileName;
    }
    protected void Load()
    {
        using (GameDataTableParser dataParser = new GameDataTableParser(DataFileName()))
        {          
            while (dataParser.HasNextRow())
            {
                P p;
                p = MakeEntity(dataParser);
                m_dataDic.Add(p.Id, p);
                m_dataList.Add(p);
            }
        }

    }
    public List<P> GetDataList
    {
        get
        {
            if (m_dataList.Count > 1 && m_dataList != null)
            {
                return m_dataList;
            }
            else
            {
                return null;
            }
        }
    }

    public P GetEntityById(int id)
    {
        if (m_dataDic.ContainsKey(id))
        {
            return m_dataDic[id];
        }
        return null;
    }
    public virtual void Dispose()
    {
        m_dataList.Clear();
        m_dataList = null;
        m_dataDic.Clear();
        m_dataDic = null;
    }
}
