using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Text;

public class NetWorkSocket : SingletonMMO<NetWorkSocket>
{

    //接受数据的字节数组缓冲区
    private byte[] m_receiveBuffer = new byte[1024 * 1024];

    //接受数据的线程
    private Thread m_receiveThread;

    //接受数据包的缓存数据流
    private MMO_MemoryStream m_ReceiveMS = new MMO_MemoryStream();

    //接受服务器消息队列
    private Queue<byte[]> m_receiveQueue = new Queue<byte[]>();

    protected override void OnAwake()
    {
        base.OnAwake();
        DontDestroyOnLoad(this);
    }

    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        while (true)
        {
            if (m_receiveCount <= 5)
            {
                m_receiveCount++;
                lock (m_receiveQueue)
                {
                    if (m_receiveQueue.Count > 0)
                    {
                        //得到队列中的数据包
                        byte[] buffer = m_receiveQueue.Dequeue();

                        //存放异或后真正数据包的缓存 协议编号 + 数据内容
                        byte[] bufferNew = new byte[buffer.Length - 3];

                        bool isCompress = false;

                        ushort crc = 0;

                        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
                        {
                            isCompress = ms.ReadBool();
                            crc = ms.ReadUShort();

                            //读取数据包到缓存
                            ms.Read(bufferNew, 0, bufferNew.Length);
                        }

                        //对数据进行校验
                        ushort NewCrc = Crc16.CalculateCrc16(bufferNew);

                        //原来数据crc和接受数据的crc进行比对
                        if (crc == NewCrc)
                        {
                            //异或得到原生数据
                            bufferNew = SecurityUtil.Xor(bufferNew);
                            if (isCompress)
                            {
                                //解压字节数组
                                bufferNew = ZlibHelper.DeCompressBytes(bufferNew);
                            }

                            ushort protoCode = 0;
                            byte[] protoContent = null;
                            using (MMO_MemoryStream ms = new MMO_MemoryStream(bufferNew))
                            {
                                protoCode = ms.ReadUShort();
                                ms.Read(protoContent, 0, protoContent.Length);

                                //开始派发事件
                                SocketDispatcher.Instance.Dispatch(protoCode, protoContent);
                            }

                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                m_receiveCount = 0;
                break;
            }
        }

    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
        if (m_client != null && m_client.Connected)
        {
            m_client.Shutdown(SocketShutdown.Both);
            m_client.Close();
            Debug.Log("客户端socket已经关闭");
        }
    }
    private byte[] buffer = new byte[1024 * 1024];

    //发送消息队列
    private Queue<byte[]> m_sendQueue = new Queue<byte[]>();

    //检查队列的委托
    private Action m_checkSendQueue;

    private int m_receiveCount = 0;

    /// <summary>
    /// 客户端socket
    /// </summary>
    private Socket m_client;

    private const int m_compressLen = 200; //200字节我们才压缩数据包

    /// <summary>
    /// 连接到socket服务器
    /// </summary>
    /// <param name="ip">ip</param>
    /// <param name="port">端口号</param>
    public void Connect(string ip,int port)
    {
        //socket存在，并且已经处于连接中
        if (m_client != null && m_client.Connected) return;
        m_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            m_client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
            m_checkSendQueue = OnCheckSendQueueCallback;
            Debug.Log("连接成功");
            ReceiveMsg();
        }
        catch (System.Exception e)
        {
            Debug.Log("连接失败" + e.Message);
        }
    }  

    /// <summary>
    /// 检查队列的委托回调
    /// </summary>
    private void OnCheckSendQueueCallback()
    {
        lock(m_sendQueue)
        {
            //如果队列有数据包就发送数据包
            if(m_sendQueue.Count > 0)
            {
                Send(m_sendQueue.Dequeue());
            }
        }
    }

    /// <summary>
    /// 封装数据包
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    private byte[] MakeData(byte[] buffer)
    {
        byte[] bufferData = null;

        //进行异或加密
        buffer = SecurityUtil.Xor(buffer);

        //测试是否需要压缩标识
        bool isCompress = buffer.Length > m_compressLen;

        if (isCompress)
        {
            buffer = ZlibHelper.CompressBytes(buffer);
        }

        //计算CRC
        ushort crc = Crc16.CalculateCrc16(buffer);

        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort((ushort)(buffer.Length + 3));

            ms.WriteBool(isCompress); //写入压缩标识1字节
          
            //写入CRC校验
            ms.WriteUShort(crc);  //2字节  

            ms.Write(buffer, 0, buffer.Length);

            bufferData = ms.ToArray();
        }
        return bufferData;
    }

   /// <summary>
   /// 发送消息 不是真正发消息 只是把消息加入队列
   /// </summary>
   /// <param name="buffer"></param>
    public void SendMsg(byte[] buffer)
    {
        //得到封装后的数据包
        byte[] sendBuffer = MakeData(buffer);

        //把消息加入队列
        lock(m_sendQueue)
        {
            //把数据包加入队列
            m_sendQueue.Enqueue(sendBuffer);

            //启动委托
            m_checkSendQueue.Invoke();
        }
    }

    /// <summary>
    /// 真正发送数据包到服务器
    /// </summary>
    /// <param name="buffer"></param>
    private void Send(byte[] buffer)
    {
        m_client.BeginSend(buffer, 0, buffer.Length, SocketFlags.None,SendMsgCallback,m_client);
    }

    /// <summary>
    /// 发送数据包的回调
    /// </summary>
    /// <param name="ar"></param>
    private void SendMsgCallback(IAsyncResult ar)
    {
        m_client.EndSend(ar);

        //继续检查消息队列
        OnCheckSendQueueCallback();
    }

    /// <summary>
    /// 接受数据
    /// </summary>
    private void ReceiveMsg()
    {
        //异步接受数据
        m_client.BeginReceive(m_receiveBuffer, 0, m_receiveBuffer.Length, SocketFlags.None, ReceiveCallback, m_client);
    }
    //接受数据回调
    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            int len = m_client.EndReceive(ar);
            if (len > 0)
            {
                //已经接受到数据
                //把接受到数据  写入数据流的尾部这样下次接受数据就做Position开始
                m_ReceiveMS.Position = m_ReceiveMS.Length;

                //把指定长度的字节写入数据流
                m_ReceiveMS.Write(m_receiveBuffer, 0, len);

                //说明至少有不完整包过来了
                if (m_ReceiveMS.Length > 2)
                {
                    //开始进行循环拆包
                    while (true)
                    {
                        m_ReceiveMS.Position = 0;

                        //读取数据包包体的长度
                        int currMsgLen = m_ReceiveMS.ReadUShort();

                        //整个数据包的长度 包头 + 数据包体
                        int currFullMsgLen = 2 + currMsgLen;

                        //说明至少接受到一个完整的数据包了
                        if (m_ReceiveMS.Length >= currFullMsgLen)
                        {
                            //开包体字节数内存
                            byte[] bufferData = new byte[currMsgLen];
                            m_ReceiveMS.Position = 2;
                            m_ReceiveMS.Read(bufferData, 0, currMsgLen);

                            lock (m_receiveQueue)
                            {
                                m_receiveQueue.Enqueue(bufferData); 
                            }

                            //处理剩余字节数据
                            int remainLen = (int)m_ReceiveMS.Length - currFullMsgLen;

                            if (remainLen > 0)
                            {
                                m_ReceiveMS.Position = currFullMsgLen;

                                byte[] remainBuffer = new byte[remainLen];

                                //把剩余字节从流中读取到缓存
                                m_ReceiveMS.Read(remainBuffer, 0, remainLen);

                                //先把内存流中的字节清理掉
                                m_ReceiveMS.Position = 0;
                                m_ReceiveMS.SetLength(0);

                                //开始把剩余字节再次写入流中
                                m_ReceiveMS.Write(remainBuffer, 0, remainLen);

                                remainBuffer = null; //释放内存
                            }
                            else //没有剩余字节
                            {
                                //先把内存流中的字节清理掉
                                m_ReceiveMS.Position = 0;
                                m_ReceiveMS.SetLength(0);
                                break;
                            }
                        }
                        else
                        {
                            //还没收到完整包
                            break;  //等待下次接受数据包
                        }
                    }
                    //一个包接完了 在次接受数据包
                    ReceiveMsg();
                }
            }
            else
            {
                Debug.Log(string.Format("服务器{0}的Socket套接字已经关闭", m_client.RemoteEndPoint));
            }
        }
        catch
        {
            Debug.Log(string.Format("服务器{0}的Socket套接字已经关闭", m_client.RemoteEndPoint));
        }
    }
}
