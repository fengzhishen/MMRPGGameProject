using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIGameServerItemView : MonoBehaviour
{
    /// <summary>
    /// 服务器状态
    /// </summary>
    [SerializeField]
    private  List<Sprite> m_gameServerStatus;

    /// <summary>
    /// 当前的服务器状态
    /// </summary>
    [SerializeField]
    private Image m_currGameServerStatus;

    /// <summary>
    /// 服务器名称
    /// </summary>
    [SerializeField]
    private Text m_gameServerName;

    /// <summary>
    /// ip
    /// </summary>
    private string m_ip;

    /// <summary>
    /// 端口号
    /// </summary>
    private int m_port;

    public void SetUI(RetGameServerEntity entity)
    {
        m_currGameServerStatus.sprite = m_gameServerStatus[entity.RunStatus];

        m_ip = entity.Ip;

        m_port = entity.Port;

        m_gameServerName.text = entity.Name;
    }
}
