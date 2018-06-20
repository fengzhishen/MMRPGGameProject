using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;

public sealed class DeviceUtity
{
    /// <summary>
    /// �õ��豸Ψһ�ı�ʶ��
    /// </summary>
    public static string DeviceDentifier
    {
        get
        {
            return SystemInfo.deviceUniqueIdentifier;
        }
    }

    /// <summary>
    /// ��ȡ�豸���ͺ�
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
    /// ��ȡ�ͻ����豸�������ַ
    /// </summary>
    public static string ClientDeviceID
    {
        get
        {
            if (string.IsNullOrEmpty(m_clientDeviceID))
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                if (interfaces != null && interfaces.Length > 0)
                    m_clientDeviceID = interfaces[0].GetPhysicalAddress().ToString(); //�õ�������ma��ַ
            }

            return m_clientDeviceID;
        }
    }
}
