using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class LanguageDBModel
{
    public Language m_currentLanguage { get; set; }
    /// <summary>
    /// 根据模块和key获取值
    /// </summary>
    /// <param name="module"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetText(string module,string key)
    {
        string language = "";
        //读取到数据
        if(m_dataList != null && m_dataList.Count > 0)
        {
            for (int i = 0; i < m_dataList.Count; i++)
            {
                if(m_dataList[i].Module.Equals(module,System.StringComparison.CurrentCultureIgnoreCase)
                    && m_dataList[i].Key.Equals(key,System.StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (m_currentLanguage)
                    {
                        case Language.CN:
                            return language = m_dataList[i].CN;                      
                        case Language.EN:
                            return language = m_dataList[i].EN;
                        default:
                            break;
                    }
                }
                 
            }
        }

        return null;
    }

    /// <summary>
    /// 返回所有模块
    /// </summary>
    /// <returns></returns>
    public List<string> GetModules()
    {
        List<string> moduleList = null;
        if (m_dataList != null && m_dataList.Count > 0)
        {
            moduleList = new List<string>();

            for (int i = 0; i < m_dataList.Count; i++)
            {
                moduleList.Add(m_dataList[i].Module);
            }
        }

        return moduleList;
    }

    /// <summary>
    /// 根据模块名字返回他的全部key
    /// </summary>
    /// <param name="module"></param>
    /// <returns></returns>
    public List<string> GetKeysByModule(string module)
    {
        List<string> keys = new List<string>();
        if(m_dataList != null && m_dataList.Count > 0)
        {
            for (int i = 0; i < m_dataList.Count; i++)
            {
                if(m_dataList[i].Module.Equals(module,System.StringComparison.CurrentCultureIgnoreCase))
                {
                    keys.Add(m_dataList[i].Key);
                }
            }
        }

        return keys;
    }
}
