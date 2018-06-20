using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 加密文件数据解析类
/// </summary>
public sealed class GameDataTableParser : IDisposable
{
    #region 字段
    /// <summary>
    /// 行数
    /// </summary>
    private int m_Row;

    /// <summary>
    /// 列数
    /// </summary>
    private int m_Column;

    /// <summary>
    /// 字段名称
    /// </summary>
    private string[] m_FieldName;

    /// <summary>
    /// 游戏数据
    /// </summary>
    private string[,] m_GameData;

    /// <summary>
    /// 当前行号
    /// </summary>
    private int m_CurRowNo = 2;

    /// <summary>
    /// 字段名称字典 保存我们的表的每列名字 int存的是字段名字所在的列号
    /// </summary>
    private Dictionary<string, int> m_FieldNameDic;
    #endregion

    #region xorScale 异或因子   用于对字节数组进行解密的密钥
    /// <summary>
    /// 异或因子
    /// </summary>
    private byte[] xorScale = new byte[] { 45, 66, 38, 55, 23, 254, 9, 165, 90, 19, 41, 45, 201, 58, 55, 37, 254, 185, 165, 169, 19, 171 };//.data文件的xor加解密因子
    #endregion

    #region GetFieldValue 获取字段的值
    /// <summary>
    /// 获取字段值
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public string GetFieldValue(string fieldName)
    {
        if(m_CurRowNo < 3 || m_CurRowNo >= m_Row)
        {
            throw new ArgumentOutOfRangeException("数据索引越界。");
        }
        return m_GameData[m_CurRowNo, m_FieldNameDic[fieldName]];
    }
    #endregion

    #region HasNextRow 存在下一条
    /// <summary>
    /// 存在下一条
    /// </summary>
    public bool HasNextRow()
    {
       return m_CurRowNo++ < m_Row - 1;
    
    }

    #endregion

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="filePath">需要解析的文件路径</param>
    public GameDataTableParser(string filePath)
    {
        byte[] dataFileBuffer = null;
        m_FieldNameDic = new Dictionary<string, int>();

        //------------------
        //第1步：读取文件
        //------------------
        try
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                dataFileBuffer = new byte[fs.Length];
                fs.Read(dataFileBuffer, 0, dataFileBuffer.Length);
            }
        }
        catch (FileNotFoundException e)
        {
            Debug.LogErrorFormat("需要解析的文件找不到,详情｛0｝，filePath =｛1｝" + e.Message,filePath);
        }

        //------------------
        //第2步：解压缩
        //------------------
        dataFileBuffer = ZlibHelper.DeCompressBytes(dataFileBuffer);

        //------------------
        //第3步：xor解密
        //------------------
        for (int i = 0; i < dataFileBuffer.Length; i++)
        {
            dataFileBuffer[i] = (byte)(dataFileBuffer[i] ^ xorScale[i % xorScale.Length]);
        }

        using (MMO_MemoryStream  ms = new MMO_MemoryStream(dataFileBuffer))
        {
            //读取数据的行和列
            m_Row = ms.ReadInt();
            m_Column = ms.ReadInt();
            m_GameData = new string[m_Row, m_Column];
            m_FieldName = new string[m_Column]; //开字段数内存
            for (int i = 0; i < m_Row; i++)
            {
                for (int j = 0; j < m_Column; j++)
                {
                    string str = ms.ReadUTF8String();
                    //现在我们需要拿到第一行字段的名字
                    if(i == 0)
                    {
                        m_FieldName[j] = str;
                        m_FieldNameDic[str] = j;
                    }

                    if(i > 2) //开始读取文件中的内容
                    {
                        m_GameData[i, j] = str;
                    }
                }
            }
        }
    }
    public void Dispose()
    {
        m_FieldName = null;
        m_GameData = null;
        m_FieldNameDic.Clear();
        m_FieldNameDic = null;
    }
}

