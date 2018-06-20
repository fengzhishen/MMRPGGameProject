using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;
/// <summary>
/// 内存流：数据转换（byte short int long float等数据）
/// </summary>
public class MMO_MemoryStream : MemoryStream
{
    public MMO_MemoryStream(byte[] bytes):base(bytes)
    {

    }
    public MMO_MemoryStream() : base()
    {

    }
    #region Short数据类型读写
    /// <summary>
    /// 从流中读取一个short数据
    /// </summary>
    /// <returns>short类型数据</returns>
    public short ReadShort()
    {
        byte[] buffer = new byte[2];
        base.Read(buffer, 0, buffer.Length);
        return BitConverter.ToInt16(buffer, 0);
    }

    /// <summary>
    /// 把short类型数据写入到当前的流中
    /// </summary>
    /// <param name="value"></param>
    public void WriteShort(short value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        base.Write(buffer, 0, buffer.Length);
    }

    #endregion

    #region UShort数据类型读写
    /// <summary>
    /// 从流中读取一个short数据
    /// </summary>
    /// <returns>ushort类型数据</returns>
    public ushort ReadUShort()
    {
        byte[] buffer = new byte[2];
        base.Read(buffer, 0, buffer.Length);
        return BitConverter.ToUInt16(buffer, 0);
    }

    /// <summary>
    /// 把ushort类型数据写入到当前的流中
    /// </summary>
    /// <param name="value"></param>
    public void WriteUShort(UInt16 value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        base.Write(buffer, 0, buffer.Length);
    }

    #endregion

    #region Int数据类型读写
    /// <summary>
    /// 从流中读取一个Int数据
    /// </summary>
    /// <returns>int数据类型的数据</returns>
    public int ReadInt()
    {
        int i = int.MaxValue;
        int length = BitConverter.GetBytes(i).Length;
        byte[] buffer = new byte[length]; //支持不知道数据所占字节数适用
        base.Read(buffer, 0, buffer.Length);
        return BitConverter.ToInt32(buffer, 0);
    }

    /// <summary>
    /// 把Int类型数据写入到当前的流中
    /// </summary>
    /// <param name="value"></param>
    public void WriteInt(Int32 value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        base.Write(buffer, 0, buffer.Length);
    }

    #endregion

    #region UInt数据类型读写
    /// <summary>
    /// 从流中读取一个UInt数据
    /// </summary>
    /// <returns></returns>
    public uint ReadUInt()
    {
        uint i = uint.MaxValue;
        int length = BitConverter.GetBytes(i).Length;
        byte[] buffer = new byte[length]; //支持不知道数据所占字节数适用
        base.Read(buffer, 0, buffer.Length);
        return BitConverter.ToUInt32(buffer, 0);
    }

    /// <summary>
    /// 把UInt类型数据写入到当前的流中
    /// </summary>
    /// <param name="value"></param>
    public void WriteUInt(UInt32 value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        base.Write(buffer, 0, buffer.Length);
    }

    #endregion

    #region Long数据类型读写
    /// <summary>
    /// 从流中读取一个long数据
    /// </summary>
    /// <returns></returns>
    public long ReadLong()
    {
        long i = long.MaxValue;
        int length = BitConverter.GetBytes(i).Length;
        byte[] buffer = new byte[length]; //支持不知道数据所占字节数适用
        base.Read(buffer, 0, buffer.Length);
        return BitConverter.ToInt64(buffer, 0);
    }

    /// <summary>
    /// 把long类型数据写入到当前的流中
    /// </summary>
    /// <param name="value"></param>
    public void WriteLong(Int64 value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        base.Write(buffer, 0, buffer.Length);
    }

    #endregion

    #region ULong数据类型读写
    /// <summary>
    /// 从流中读取一个ulong数据
    /// </summary>
    /// <returns></returns>
    public ulong ReadULong()
    {
        ulong i = ulong.MaxValue;
        int length = BitConverter.GetBytes(i).Length;
        byte[] buffer = new byte[length]; //支持不知道数据所占字节数适用
        base.Read(buffer, 0, buffer.Length);
        return BitConverter.ToUInt64(buffer, 0);
    }

    /// <summary>
    /// 把ulong类型数据写入到当前的流中
    /// </summary>
    /// <param name="value"></param>
    public void WriteULong(UInt64 value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        base.Write(buffer, 0, buffer.Length);
    }

    #endregion

    #region Float数据类型读写
    /// <summary>
    /// 从流中读取一个float数据
    /// </summary>
    /// <returns></returns>
    public Single ReadFloat()
    {
        float i = Single.MaxValue;
        int length = BitConverter.GetBytes(i).Length;
        byte[] buffer = new byte[length]; //支持不知道数据所占字节数适用
        base.Read(buffer, 0, buffer.Length);
        return BitConverter.ToSingle(buffer, 0);
    }

    /// <summary>
    /// 把float类型数据写入到当前的流中
    /// </summary>
    /// <param name="value"></param>
    public void WriteFloat(float value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        base.Write(buffer, 0, buffer.Length);
    }

    #endregion

    #region Double数据类型读写
    /// <summary>
    /// 从流中读取一个double数据
    /// </summary>
    /// <returns></returns>
    public Double ReadDouble()
    {      
        byte[] buffer = new byte[8]; 
        base.Read(buffer, 0, buffer.Length);
        return BitConverter.ToDouble(buffer, 0);
    }

    /// <summary>
    /// 把double类型数据写入到当前的流中
    /// </summary>
    /// <param name="value"></param>
    public void WriteDouble(Double value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        base.Write(buffer, 0, buffer.Length);
    }

    #endregion

    #region Bool数据类型读写
    /// <summary>
    /// 从流中读取一个bool数据
    /// </summary>
    /// <returns></returns>
    public bool ReadBool()
    {
        return base.ReadByte() == 1?true:false;
    }

    /// <summary>
    /// 把bool类型数据写入到当前的流中
    /// </summary>
    /// <param name="value"></param>
    public void WriteBool(bool value)
    {
        base.WriteByte((byte)(value == true ? 1:0));
    }

    #endregion

    /// <summary>
    /// UTF-8编码 读取流中的字符串
    /// </summary>
    /// <returns></returns>
    public string ReadUTF8String()
    {
        ushort len = this.ReadUShort();
        byte[] buffer = new byte[len];
        base.Read(buffer, 0, len);
        return Encoding.UTF8.GetString(buffer);
    }

    /// <summary>
    /// 将字符串写入流中
    /// </summary>
    /// <param name="strData">需要写入流中的字符串数据</param>
    public void WriteUTF8String(string strData)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(strData);
        ushort bytesLength = (ushort)bytes.Length;
        if(bytesLength > 65535)
        {
            throw new InvalidCastException("字符串内容过多");
        }
        this.WriteUShort(bytesLength);
        base.Write(bytes, 0, bytesLength);
    }
}
