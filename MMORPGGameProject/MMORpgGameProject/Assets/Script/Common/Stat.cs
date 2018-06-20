using UnityEngine;
using System.Collections;

/// <summary>
/// 用于对接第三方平台统计使用
/// </summary>
public class Stat
{
    public static void Init()
    {

    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="nickName"></param>
    public static void Reg(int userId,string nickName)
    {

    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="nickName"></param>
    public static void LogOn(int userId,string nickName)
    {

    }

    /// <summary>
    /// 修改昵称
    /// </summary>
    /// <param name="nickName"></param>
    public static void ChangeNickName(string nickName)
    {

    }

    /// <summary>
    /// 升级
    /// </summary>
    /// <param name="level"></param>
    public static void UpLevel(int level)
    {

    }

    /// <summary>
    /// 任务开始
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="taskName"></param>
    public static void TaskBegin(int taskId,string taskName)
    {

    }
    
    /// <summary>
    /// 任务结束
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="taskName"></param>
    /// <param name="status"></param>
    public static void TaskEnd(int taskId,string taskName,int status)
    {

    }

    /// <summary>
    /// 关卡开始
    /// </summary>
    /// <param name="gameLevelId"></param>
    /// <param name="gameLevelName"></param>
    public static void GameLevelBegin(int gameLevelId,string gameLevelName)
    {

    }
    /// <summary>
    /// 关卡结束
    /// </summary>
    /// <param name="gameLevelId"></param>
    /// <param name="gameLevelName"></param>
    public static void GameLevelEnd(int gameLevelId, string gameLevelName,int status,int star)
    {

    }

    /// <summary>
    /// 开始充值
    /// </summary>
    /// <param name="orderId">订单号</param>
    /// <param name="productId"><产品编号/param>
    /// <param name="money">重置金额</param>
    /// <param name="type">币种</param>
    /// <param name="virtualMoney">虚拟货币</param>
    /// <param name="channelId">渠道号</param>
    public static void ChargeBegin(string orderId,string productId,double money,string type,double virtualMoney,int channelId)
    {

    }

    /// <summary>
    /// 重置完成
    /// </summary>
    public static void ChargeEnd()
    {

    }

    /// <summary>
    /// 购买道具
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="itemName"></param>
    /// <param name="price"></param>
    /// <param name="count"></param>
    public static void BuyItem(int itemId,string itemName,int price,int count)
    {

    }

    /// <summary>
    /// 道具使用
    /// </summary>
    /// <param name="itemId">道具编号</param>
    /// <param name="itemName"></param>
    /// <param name="price"></param>
    /// <param name="count">数量</param>
    /// <param name="useType">用途</param>
    public static void ItemUsed(int itemId,string itemName,int price,int count,int useType)
    {

    }

    /// <summary>
    /// 自定义事件
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void AddEvent(string key,string value)
    {

    }
}
