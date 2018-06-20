using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 保存来自服务器的数据实体类  区服实体类
/// </summary>
public class RetGameServerEntity
{
    /// <summary>
    /// 区分编号
    /// </summary>
    public int Id;

    /// <summary>
    /// 区服状态
    /// </summary>
    public int RunStatus;

    /// <summary>
    /// 是否推荐
    /// </summary>
    public bool IsCommand;

    /// <summary>
    /// 是否新服
    /// </summary>
    public bool IsNew;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name;

    /// <summary>
    /// ip
    /// </summary>
    public string Ip;

    /// <summary>
    /// 端口号
    /// </summary>
    public int Port;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime;

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdateTime;
}
