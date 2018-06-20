using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;
/// <summary>
/// �ڴ���������ת����byte short int long float�����ݣ�
/// </summary>
public class MMO_MemoryStream : MemoryStream
{
    public MMO_MemoryStream(byte[] bytes):base(bytes)
    {

    }
    public MMO_MemoryStream() : base()
    {

    }
    #region Short�������Ͷ�д
    /// <summary>
    /// �����ж�ȡһ��short����
    /// </summary>
    /// <returns>short��������</returns>
    public short ReadShort()
    {
        byte[] buffer = new byte[2];
        base.Read(buffer, 0, buffer.Length);
        return BitConverter.ToInt16(buffer, 0);
    }

    /// <summary>
    /// ��short��������д�뵽��ǰ������
    /// </summary>
    /// <param name="value"></param>
    public void WriteShort(short value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        base.Write(buffer, 0, buffer.Length);
    }

    #endregion

    #region UShort�������Ͷ�д
    /// <summary>
    /// �����ж�ȡһ��short����
    /// </summary>
    /// <returns>ushort��������</returns>
    public ushort ReadUShort()
    {
        byte[] buffer = new byte[2];
        base.Read(buffer, 0, buffer.Length);
        return BitConverter.ToUInt16(buffer, 0);
    }

    /// <summary>
    /// ��ushort��������д�뵽��ǰ������
    /// </summary>
    /// <param name="value"></param>
    public void WriteUShort(UInt16 value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        base.Write(buffer, 0, buffer.Length);
    }

    #endregion

    #region Int�������Ͷ�д
    /// <summary>
    /// �����ж�ȡһ��Int����
    /// </summary>
    /// <returns>int�������͵�����</returns>
    public int ReadInt()
    {
        int i = int.MaxValue;
        int length = BitConverter.GetBytes(i).Length;
        byte[] buffer = new byte[length]; //֧�ֲ�֪��������ռ�ֽ�������
        base.Read(buffer, 0, buffer.Length);
        return BitConverter.ToInt32(buffer, 0);
    }

    /// <summary>
    /// ��Int��������д�뵽��ǰ������
    /// </summary>
    /// <param name="value"></param>
    public void WriteInt(Int32 value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        base.Write(buffer, 0, buffer.Length);
    }

    #endregion

    #region UInt�������Ͷ�д
    /// <summary>
    /// �����ж�ȡһ��UInt����
    /// </summary>
    /// <returns></returns>
    public uint ReadUInt()
    {
        uint i = uint.MaxValue;
        int length = BitConverter.GetBytes(i).Length;
        byte[] buffer = new byte[length]; //֧�ֲ�֪��������ռ�ֽ�������
        base.Read(buffer, 0, buffer.Length);
        return BitConverter.ToUInt32(buffer, 0);
    }

    /// <summary>
    /// ��UInt��������д�뵽��ǰ������
    /// </summary>
    /// <param name="value"></param>
    public void WriteUInt(UInt32 value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        base.Write(buffer, 0, buffer.Length);
    }

    #endregion

    #region Long�������Ͷ�д
    /// <summary>
    /// �����ж�ȡһ��long����
    /// </summary>
    /// <returns></returns>
    public long ReadLong()
    {
        long i = long.MaxValue;
        int length = BitConverter.GetBytes(i).Length;
        byte[] buffer = new byte[length]; //֧�ֲ�֪��������ռ�ֽ�������
        base.Read(buffer, 0, buffer.Length);
        return BitConverter.ToInt64(buffer, 0);
    }

    /// <summary>
    /// ��long��������д�뵽��ǰ������
    /// </summary>
    /// <param name="value"></param>
    public void WriteLong(Int64 value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        base.Write(buffer, 0, buffer.Length);
    }

    #endregion

    #region ULong�������Ͷ�д
    /// <summary>
    /// �����ж�ȡһ��ulong����
    /// </summary>
    /// <returns></returns>
    public ulong ReadULong()
    {
        ulong i = ulong.MaxValue;
        int length = BitConverter.GetBytes(i).Length;
        byte[] buffer = new byte[length]; //֧�ֲ�֪��������ռ�ֽ�������
        base.Read(buffer, 0, buffer.Length);
        return BitConverter.ToUInt64(buffer, 0);
    }

    /// <summary>
    /// ��ulong��������д�뵽��ǰ������
    /// </summary>
    /// <param name="value"></param>
    public void WriteULong(UInt64 value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        base.Write(buffer, 0, buffer.Length);
    }

    #endregion

    #region Float�������Ͷ�д
    /// <summary>
    /// �����ж�ȡһ��float����
    /// </summary>
    /// <returns></returns>
    public Single ReadFloat()
    {
        float i = Single.MaxValue;
        int length = BitConverter.GetBytes(i).Length;
        byte[] buffer = new byte[length]; //֧�ֲ�֪��������ռ�ֽ�������
        base.Read(buffer, 0, buffer.Length);
        return BitConverter.ToSingle(buffer, 0);
    }

    /// <summary>
    /// ��float��������д�뵽��ǰ������
    /// </summary>
    /// <param name="value"></param>
    public void WriteFloat(float value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        base.Write(buffer, 0, buffer.Length);
    }

    #endregion

    #region Double�������Ͷ�д
    /// <summary>
    /// �����ж�ȡһ��double����
    /// </summary>
    /// <returns></returns>
    public Double ReadDouble()
    {      
        byte[] buffer = new byte[8]; 
        base.Read(buffer, 0, buffer.Length);
        return BitConverter.ToDouble(buffer, 0);
    }

    /// <summary>
    /// ��double��������д�뵽��ǰ������
    /// </summary>
    /// <param name="value"></param>
    public void WriteDouble(Double value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        base.Write(buffer, 0, buffer.Length);
    }

    #endregion

    #region Bool�������Ͷ�д
    /// <summary>
    /// �����ж�ȡһ��bool����
    /// </summary>
    /// <returns></returns>
    public bool ReadBool()
    {
        return base.ReadByte() == 1?true:false;
    }

    /// <summary>
    /// ��bool��������д�뵽��ǰ������
    /// </summary>
    /// <param name="value"></param>
    public void WriteBool(bool value)
    {
        base.WriteByte((byte)(value == true ? 1:0));
    }

    #endregion

    /// <summary>
    /// UTF-8���� ��ȡ���е��ַ���
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
    /// ���ַ���д������
    /// </summary>
    /// <param name="strData">��Ҫд�����е��ַ�������</param>
    public void WriteUTF8String(string strData)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(strData);
        ushort bytesLength = (ushort)bytes.Length;
        if(bytesLength > 65535)
        {
            throw new InvalidCastException("�ַ������ݹ���");
        }
        this.WriteUShort(bytesLength);
        base.Write(bytes, 0, bytesLength);
    }
}
