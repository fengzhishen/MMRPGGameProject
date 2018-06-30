//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-12 08:58:43
//备    注：
//===================================================
using UnityEngine;
using System.Collections;

/// <summary>
/// 主角信息
/// </summary>
public class RoleInfoMainPlayer : RoleInfoBase
{
    public byte JobId; //职业编号
    public int Money; //元宝
    public int Gold; //金币

    public RoleInfoMainPlayer()
    {

    }

    public RoleInfoMainPlayer(RoleOperation_SelectRoleInfoReturnProto roleProto)
    {
        this.RoldId = roleProto.RoldId; //角色编号
        this.RoleNickName = roleProto.RoleNickName; //角色昵称
        this.JobId = roleProto.JobId; //职业编号
        this.Level = roleProto.Level; //等级
        this.Money = roleProto.Money; //元宝
        this.Gold = roleProto.Gold; //金币
        this.Exp = roleProto.Exp; //经验
        this.MaxHP = roleProto.MaxHP; //最大HP
        this.MaxMP = roleProto.MaxMP; //最大MP
        this.CurrHP = roleProto.CurrHP; //当前HP
        this.CurrMP = roleProto.CurrMP; //当前MP
        this.Attack = roleProto.Attack; //攻击力
        this.Defense = roleProto.Defense; //防御
        this.Hit = roleProto.Hit; //命中
        this.Dodge = roleProto.Dodge; //闪避
        this.Cri = roleProto.Cri; //暴击
        this.Res = roleProto.Res; //抗性
        this.Fighting = roleProto.Fighting; //综合战斗力      
    }
}