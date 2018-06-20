using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;

public sealed class DeviceUtity
{
    /// <summary>
    /// 得到设备唯一的标识符
    /// </summary>
    public static string DeviceDentifier
    {
        get
        {
            return SystemInfo.deviceUniqueIdentifier;
        }
    }

    /// <summary>
    /// 获取设备的型号
    /// </summary>
    public static string DeviceModel
    {
        get
        {

#if UNITY_IPHONE && !UNITY_EDITOR
            return Device.generation.ToString();
#else 
            return SystemInfo.deviceModel;
#endif
        }
    }

    private static string m_clientDeviceID = string.Empty;
    /// <summary>
    /// 获取客户端设备的物理地址
    /// </summary>
    public static string ClientDeviceID
    {
        get
        {
            if (string.IsNullOrEmpty(m_clientDeviceID))
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                if (interfaces != null && interfaces.Length > 0)
                    m_clientDeviceID = interfaces[0].GetPhysicalAddress().ToString(); //得到网卡的ma地址
            }

            return m_clientDeviceID;
        }
    }
}
