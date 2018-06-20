using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// �������Է�����������ʵ����  ����ʵ����
/// </summary>
public class RetGameServerEntity
{
    /// <summary>
    /// ���ֱ��
    /// </summary>
    public int Id;

    /// <summary>
    /// ����״̬
    /// </summary>
    public int RunStatus;

    /// <summary>
    /// �Ƿ��Ƽ�
    /// </summary>
    public bool IsCommand;

    /// <summary>
    /// �Ƿ��·�
    /// </summary>
    public bool IsNew;

    /// <summary>
    /// ����
    /// </summary>
    public string Name;

    /// <summary>
    /// ip
    /// </summary>
    public string Ip;

    /// <summary>
    /// �˿ں�
    /// </summary>
    public int Port;

    /// <summary>
    /// ����ʱ��
    /// </summary>
    public DateTime CreateTime;

    /// <summary>
    /// ����ʱ��
    /// </summary>
    public DateTime UpdateTime;
}
