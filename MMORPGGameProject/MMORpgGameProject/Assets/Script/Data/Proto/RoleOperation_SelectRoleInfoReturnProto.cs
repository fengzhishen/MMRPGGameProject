//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2017-07-05 11:59:23
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器返回角色信息
/// </summary>
public struct RoleOperation_SelectRoleInfoReturnProto : IProto
{
    public ushort ProtoCode { get { return 10010; } }

    public bool IsSucess; //是否成功
    public short MessageId; //错误编号
    public int RoldId; //角色编号
    public string RoleNickName; //角色昵称
    public byte JobId; //职业编号
    public int Level; //等级
    public int Money; //元宝
    public int Gold; //金币
    public int Exp; //经验
    public int MaxHP; //最大HP
    public int MaxMP; //最大MP
    public int CurrHP; //当前HP
    public int CurrMP; //当前MP
    public int Attack; //攻击力
    public int Defense; //防御
    public int Hit; //命中
    public int Dodge; //闪避
    public int Cri; //暴击
    public int Res; //抗性
    public int Fighting; //综合战斗力
    public int LastInWorldMapId; //最后进入的世界地图编号
    public string LastInWorldMapPos; //最后进入的世界地图坐标

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteBool(IsSucess);
            if(IsSucess)
            {
                ms.WriteInt(RoldId);
                ms.WriteUTF8String(RoleNickName);
                ms.WriteByte(JobId);
                ms.WriteInt(Level);
                ms.WriteInt(Money);
                ms.WriteInt(Gold);
                ms.WriteInt(Exp);
                ms.WriteInt(MaxHP);
                ms.WriteInt(MaxMP);
                ms.WriteInt(CurrHP);
                ms.WriteInt(CurrMP);
                ms.WriteInt(Attack);
                ms.WriteInt(Defense);
                ms.WriteInt(Hit);
                ms.WriteInt(Dodge);
                ms.WriteInt(Cri);
                ms.WriteInt(Res);
                ms.WriteInt(Fighting);
                ms.WriteInt(LastInWorldMapId);
                ms.WriteUTF8String(LastInWorldMapPos);
            }
            else
            {
                ms.WriteShort(MessageId);
            }
            return ms.ToArray();
        }
    }

    public static RoleOperation_SelectRoleInfoReturnProto GetProto(byte[] buffer)
    {
        RoleOperation_SelectRoleInfoReturnProto proto = new RoleOperation_SelectRoleInfoReturnProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.IsSucess = ms.ReadBool();
            if(proto.IsSucess)
            {
                proto.RoldId = ms.ReadInt();
                proto.RoleNickName = ms.ReadUTF8String();
                proto.JobId = (byte)ms.ReadByte();
                proto.Level = ms.ReadInt();
                proto.Money = ms.ReadInt();
                proto.Gold = ms.ReadInt();
                proto.Exp = ms.ReadInt();
                proto.MaxHP = ms.ReadInt();
                proto.MaxMP = ms.ReadInt();
                proto.CurrHP = ms.ReadInt();
                proto.CurrMP = ms.ReadInt();
                proto.Attack = ms.ReadInt();
                proto.Defense = ms.ReadInt();
                proto.Hit = ms.ReadInt();
                proto.Dodge = ms.ReadInt();
                proto.Cri = ms.ReadInt();
                proto.Res = ms.ReadInt();
                proto.Fighting = ms.ReadInt();
                proto.LastInWorldMapId = ms.ReadInt();
                proto.LastInWorldMapPos = ms.ReadUTF8String();
            }
            else
            {
                proto.MessageId = ms.ReadShort();
            }
        }
        return proto;
    }
}