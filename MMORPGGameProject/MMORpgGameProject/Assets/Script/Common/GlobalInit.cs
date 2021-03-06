//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-01 22:26:02
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System;
using System.Net.NetworkInformation;
using UnityEngine.iOS;
using System.Collections.Generic;

public class GlobalInit : MonoBehaviour 
{
    #region 常量
    /// <summary>
    /// 昵称KEY
    /// </summary>
    public const string MMO_NICKNAME = "MMO_NICKNAME";

    /// <summary>
    /// 密码KEY
    /// </summary>
    public const string MMO_PWD = "MMO_PWD";

    #endregion

    /// <summary>
    /// 帐号服务器地址
    /// </summary>
    public const string WebAccountUrl = "http://127.0.0.1:8081/";

    public const string SocketIP = "127.0.0.1";
    public const int Port = 1011;

    public static GlobalInit Instance;

    /// <summary>
    /// 玩家注册时候的昵称
    /// </summary>
    [HideInInspector]
    public string CurrRoleNickName;

    /// <summary>
    /// 当前玩家
    /// </summary>
    [HideInInspector]
    public RoleCtrl CurrPlayer;

    /// <summary>
    /// 游戏职业镜像对象
    /// </summary>
    public IDictionary<int, GameObject> m_JobObjectDic = new Dictionary<int, GameObject>();

    /// <summary>
    /// 地面shader
    /// </summary>
    public Shader T4MGroundShader;
    /// <summary>
    /// UI动画曲线
    /// </summary>
    public AnimationCurve UIAnimationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

    /// <summary>
    /// 主角信息
    /// </summary>
    [HideInInspector]
    public RoleInfoMainPlayer MainPlayerInfo;

    /// <summary>
    /// 当前选择的区服
    /// </summary>
    [HideInInspector]
    public RetGameServerEntity m_currentSelectGameServer;

    /// <summary>
    /// 当前帐号
    /// </summary>
    [HideInInspector]
    public RetAccountEntity m_currentAccountEntity;
    /// <summary>
    /// 保存服务器的时间
    /// </summary>
    private  long ServerTime = 0;

    /// <summary>
    /// 当前服务器的时间
    /// </summary>
    public long CurrServerTime
    {
        get
        {
            return ServerTime + (long)RealTime.time;
        }
    }

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

	void Start ()
	{
        //游戏一启动我们就先从服务器那边获取时间戳
        NetWorkHttp.Instance.SendData(WebAccountUrl + "api/time", false, null, OnGetTimeCallback);
	}

    /// <summary>
    /// 当从服务器获取到时间戳之后的回调处理
    /// </summary>
    /// <param name="obj"></param>
    private void OnGetTimeCallback(NetWorkHttp.CallBackArgs obj)
    {
        if(obj.HasError)
        {
            Debug.Log(obj.ErrorMsg);
        }
        else
        {
            ServerTime = long.Parse(obj.Value.Trim('\"'));
        }
    }  


    public void Update()
    {
         /*********采集世界地图相关数据***************/
         //采集当前玩家坐标
         if(Input.GetKeyDown(KeyCode.C))
        {
            if (GlobalInit.Instance.CurrPlayer == null) return;

            //得到主角当前的位置信息 
            Transform trans = GlobalInit.Instance.CurrPlayer.transform;

            //采集位置坐标组拼成字符串
            string pos = string.Format("{0}_{1}_{2}_{3}", trans.position.x, trans.position.y, trans.position.z,trans.eulerAngles.y);

            Debug.Log("位置信息=" + pos);
        }
    }
}