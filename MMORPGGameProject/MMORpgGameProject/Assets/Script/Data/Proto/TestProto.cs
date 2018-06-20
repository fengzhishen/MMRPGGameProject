using UnityEngine;
using System.Collections;
using System;
using LitJson;

public interface IProto
{
    //–≠“È±‡∫≈
    ushort ProtoCode { get;}
}

public struct TestProto:IProto
{
    public ushort ProtoCode
    {
        get { return 1001; }
    }

    public int Id;
    public string Name;
    public int Type;
    public double Price;

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteInt(Id);
            ms.WriteUTF8String(Name);
            ms.WriteInt(Type);
            ms.WriteDouble(Price);

            return ms.ToArray();               
        }
    }

    
    public TestProto GetProto(byte[] buffer)
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            this.Id = ms.ReadInt();
            this.Name = ms.ReadUTF8String();
            this.Type = ms.ReadInt();
            this.Price = ms.ReadDouble();

            return this;
        }
    }
    public override string ToString()
    {
        return string.Format("{0}",LitJson.JsonMapper.ToJson(this));
    }
}
