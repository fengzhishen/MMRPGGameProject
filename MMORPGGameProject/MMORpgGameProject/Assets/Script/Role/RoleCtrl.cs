//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-11-02 20:22:50
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using Pathfinding;


/// <summary>
/// 角色控制器
/// </summary>
[RequireComponent(typeof(FunnelModifier))]
[RequireComponent(typeof(Seeker))]
public class RoleCtrl : MonoBehaviour 
{
    #region 成员变量或属性
    /// <summary>
    /// 昵称挂点
    /// </summary>
    [SerializeField]
    private Transform m_HeadBarPos;

    /// <summary>
    /// 头顶UI条
    /// </summary>
    private GameObject m_HeadBar;

    /// <summary>
    /// 动画
    /// </summary>
    [SerializeField]
    public Animator Animator;

    /// <summary>
    /// 移动的目标点
    /// </summary>
    [HideInInspector]
    public Vector3 TargetPos = Vector3.zero;

    /// <summary>
    /// 控制器
    /// </summary>
    [HideInInspector]
    public CharacterController CharacterController;

    /// <summary>
    /// 移动速度
    /// </summary>
    [SerializeField]
    public float Speed = 10f;

    /// <summary>
    /// 出生点
    /// </summary>
    [HideInInspector]
    public Vector3 BornPoint;

    /// <summary>
    /// 视野范围
    /// </summary>
    public float ViewRange;

    /// <summary>
    /// 巡逻范围
    /// </summary>
    public float PatrolRange;

    /// <summary>
    /// 攻击范围
    /// </summary>
    public float AttackRange;

    /// <summary>
    /// 当前角色类型
    /// </summary>
    public RoleType CurrRoleType = RoleType.None;

    /// <summary>
    /// 当前角色信息
    /// </summary>
    public RoleInfoBase CurrRoleInfo = null;

    /// <summary>
    /// 当前角色AI
    /// </summary>
    public IRoleAI CurrRoleAI = null;

    /// <summary>
    /// 锁定敌人
    /// </summary>
    [HideInInspector]
    public RoleCtrl LockEnemy;

    /// <summary>
    /// 角色受伤委托
    /// </summary>
    public System.Action OnRoleHurt;

    /// <summary>
    /// 角色死亡
    /// </summary>
    public System.Action<RoleCtrl> OnRoleDie;

    /// <summary>
    /// 当前角色有限状态机管理器
    /// </summary>
    public RoleFSMMgr CurrRoleFSMMgr = null;

    private RoleHeadBarView roleHeadBarView = null;

    #endregion

    /**************寻路相关***************************************/
    private Seeker m_seeker;

    /// <summary>
    /// 路径
    /// </summary>
    [HideInInspector]
    public ABPath AStarPath;

    /// <summary>
    /// 当前要去的路径点
    /// </summary>
    public int AStartCurrWayPoint = 1;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="roleType">角色类型</param>
    /// <param name="roleInfo">角色信息</param>
    /// <param name="ai">AI</param>
    public void Init(RoleType roleType, RoleInfoBase roleInfo, IRoleAI ai)
    {
        CurrRoleType = roleType;
        CurrRoleInfo = roleInfo;
        CurrRoleAI = ai;
    }

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();

        //寻路 计算路径的类
        m_seeker = GetComponent<Seeker>();
        Debug.Log(m_seeker.name);
        if (CurrRoleType == RoleType.MainPlayer)
        {
            if (CameraCtrl.Instance != null)
            {
                CameraCtrl.Instance.Init();
            }
        }

        CurrRoleFSMMgr = new RoleFSMMgr(this);
        ToIdle();
        InitHeadBar();
    }

    /// <summary>
    /// 开启自动小地图
    /// </summary>
    private void AutoSamllMap()
    {
        if (SamllMapHelper.Instance == null || UIMainCitySmallMapView.Instance == null) return;

        //让角色总是在这个点上
        SamllMapHelper.Instance.transform.position = transform.position;

        UIMainCitySmallMapView.Instance.transform.localPosition = new Vector3(SamllMapHelper.Instance.transform.position.x * 512, SamllMapHelper.Instance.transform.position.z * 512,1);
    }

    void Update()
    {
        if (CurrRoleFSMMgr != null)
            CurrRoleFSMMgr.OnUpdate();

        //如果角色没有AI 直接返回
        if (CurrRoleAI == null) return;
        CurrRoleAI.DoAI();   

        if (CharacterController == null) return;

        //让角色贴着地面
        if (!CharacterController.isGrounded)
        {
            CharacterController.Move((transform.position + new Vector3(0, -1000, 0)) - transform.position);
        }

        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);         

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Item")))
            {
                BoxCtrl boxCtrl = hit.collider.GetComponent<BoxCtrl>();
                if (boxCtrl != null)
                {
                    boxCtrl.Hit();
                }
            }
        }

        //让角色贴着地面
        if (!CharacterController.isGrounded)
        {
            CharacterController.Move((transform.position + new Vector3(0, -1000, 0)) - transform.position);
        }

        if (CurrRoleType == RoleType.MainPlayer)
        {
            CameraAutoFollow();
        }

        AutoSamllMap();
    }

    /// <summary>
    /// 初始化头顶UI条
    /// </summary>
    private void InitHeadBar()
    {
        if (RoleHeadBarRoot.Instance == null) return;
        if (CurrRoleInfo == null) return;
        if (m_HeadBarPos == null) return;

        //克隆预设
        m_HeadBar = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIOther, "RoleHeadBar");
        m_HeadBar.transform.parent = RoleHeadBarRoot.Instance.gameObject.transform;
        m_HeadBar.transform.localScale = Vector3.one;
        m_HeadBar.transform.localPosition = Vector3.zero;
        roleHeadBarView = m_HeadBar.GetComponent<RoleHeadBarView>();

        //给预设赋值
        roleHeadBarView.Init(m_HeadBarPos, CurrRoleInfo.RoleNickName, isShowHPBar: (CurrRoleType == RoleType.MainPlayer ? false : true));
    }


    #region 控制角色方法

    public void ToIdle()
    {
        CurrRoleFSMMgr.ChangeState(RoleState.Idle);
    }

    /// <summary>
    /// 告诉玩家去哪里
    /// </summary>
    /// <param name="targetPos"></param>
    public void MoveTo(Vector3 targetPos)
    {
        //如果目标点不是原点 进行移动
        if (targetPos == Vector3.zero) return;
        TargetPos = targetPos;
        //CurrRoleFSMMgr.ChangeState(RoleState.Run);

        //计算路径
        m_seeker.StartPath(transform.position, targetPos, (Path p) =>
          {
              if(!p.error)
              {
                  AStarPath = (ABPath)p;

                  if(Vector3.Distance(AStarPath.endPoint,new Vector3(AStarPath.originalEndPoint.x, AStarPath.endPoint.y, AStarPath.originalEndPoint.z)) > 1f)
                  {
                      AppDebug.Log("不能到达目标点");
                      AStarPath = null;
                  }

                  AStartCurrWayPoint = 1;
                  CurrRoleFSMMgr.ChangeState(RoleState.Run);
              }
              else
              {
                  //寻路失败
                  AppDebug.LogError(p.errorLog);
                  AStarPath = null;
              }
          });
    }

    public void ToAttack()
    {
        if (LockEnemy == null) return;
        CurrRoleFSMMgr.ChangeState(RoleState.Attack);

        //暂时写死
        LockEnemy.ToHurt(100, 0.5f);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attackValue">受到的攻击力</param>
    /// <param name="delay">延迟时间</param>
    public void ToHurt(int attackValue,float delay)
    {
        StartCoroutine(ToHurtCoroutine(attackValue, delay));
    }

    private IEnumerator ToHurtCoroutine(int attackValue, float delay)
    {
        yield return new WaitForSeconds(delay);

        //计算得出伤害数值
        int hurt = (int)(attackValue * Random.Range(0.5f, 1f));

        if (OnRoleHurt != null)
        {
            OnRoleHurt();
        }

        
        //CurrRoleInfo.CurrHP -= hurt;

        //roleHeadBarCtrl.Hurt(hurt, (float)CurrRoleInfo.CurrHP / CurrRoleInfo.MaxHP);

        //if (CurrRoleInfo.CurrHP <= 0)
        //{
        //    CurrRoleFSMMgr.ChangeState(RoleState.Die);
        //}
        //else
        //{
        //    CurrRoleFSMMgr.ChangeState(RoleState.Hurt);
        //}
    }

    public void ToDie()
    {
        CurrRoleFSMMgr.ChangeState(RoleState.Die);
    }

    #endregion

    #region OnDestroy 销毁
    /// <summary>
    /// 销毁
    /// </summary>
    void OnDestroy()
    {
        if (m_HeadBar != null)
        {
            Destroy(m_HeadBar);
        }
    }
    #endregion

    #region CameraAutoFollow 摄像机自动跟随
    /// <summary>
    /// 摄像机自动跟随
    /// </summary>
    private void CameraAutoFollow()
    {
        if (CameraCtrl.Instance == null) return;

        CameraCtrl.Instance.transform.position = gameObject.transform.position;
        CameraCtrl.Instance.AutoLookAt(gameObject.transform.position);
    }
    #endregion
}