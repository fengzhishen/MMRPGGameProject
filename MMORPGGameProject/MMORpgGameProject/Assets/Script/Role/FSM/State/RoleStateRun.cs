//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-12 08:54:29
//备    注：
//===================================================
using UnityEngine;
using System.Collections;

/// <summary>
/// 跑状态
/// </summary>
public class RoleStateRun : RoleStateAbstract
{
    /// <summary>
    /// 转身速度
    /// </summary>
    private float m_RotationSpeed = 0.2f;

    /// <summary>
    /// 转身的目标方向
    /// </summary>
    private Quaternion m_TargetQuaternion;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleFSMMgr">有限状态机管理器</param>
    public RoleStateRun(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {

    }

    /// <summary>
    /// 实现基类 进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();

        m_RotationSpeed = 0;
        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToRun.ToString(), true);
    }

    /// <summary>
    /// 实现基类 执行状态
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        CurrRoleAnimatorStateInfo = CurrRoleFSMMgr.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorName.Run.ToString()))
        {
            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleState.Run);
        }
        else
        {
            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), 0);
        }

        /************A星寻路相关***************/
        //说明没有可走路
        if(CurrRoleFSMMgr.CurrRoleCtrl.AStarPath == null)
        {
            //切换角色为idle状态
            CurrRoleFSMMgr.CurrRoleCtrl.ToIdle();
            return;
        }

        //如果整个路径走完了 就把角色切换为idle状态
        if(CurrRoleFSMMgr.CurrRoleCtrl.AStartCurrWayPoint >= CurrRoleFSMMgr.CurrRoleCtrl.AStarPath.vectorPath.Count)
        {
            CurrRoleFSMMgr.CurrRoleCtrl.ToIdle();
            CurrRoleFSMMgr.CurrRoleCtrl.AStarPath = null;
            return;
        }

        //角色行走方向
        Vector3 direction = Vector3.zero;

        //角色临时下一个目标点
        Vector3 temp = new Vector3(CurrRoleFSMMgr.CurrRoleCtrl.AStarPath.vectorPath[CurrRoleFSMMgr.CurrRoleCtrl.AStartCurrWayPoint].x, CurrRoleFSMMgr.CurrRoleCtrl.gameObject.transform.position.y, CurrRoleFSMMgr.CurrRoleCtrl.AStarPath.vectorPath[CurrRoleFSMMgr.CurrRoleCtrl.AStartCurrWayPoint].z);

        //开始计算角色真正行走方向
        direction = temp - CurrRoleFSMMgr.CurrRoleCtrl.gameObject.transform.position;

        //向量归一化处理
        direction = direction.normalized;

        //开始计算真正行走的速度
        direction = direction * Time.deltaTime * CurrRoleFSMMgr.CurrRoleCtrl.Speed;
      
        direction.y = 0;

        //让角色缓慢转身
        if (m_RotationSpeed <= 1)
        {
            m_RotationSpeed += 10f * Time.deltaTime;
            m_TargetQuaternion = Quaternion.LookRotation(direction);
            CurrRoleFSMMgr.CurrRoleCtrl.transform.rotation = Quaternion.Lerp(CurrRoleFSMMgr.CurrRoleCtrl.transform.rotation, m_TargetQuaternion, m_RotationSpeed);

            if (Quaternion.Angle(CurrRoleFSMMgr.CurrRoleCtrl.transform.rotation, m_TargetQuaternion) < 1)
            {
                m_RotationSpeed = 0;
            }
        }

        //判断是不是要向下一个目标点移动
        float distance = Vector3.Distance(CurrRoleFSMMgr.CurrRoleCtrl.gameObject.transform.position, temp);

        //默认这个条件就到达目标点
        if(distance <= direction.magnitude + 0.1f)
        {
            //开始更换下一个目标点
            CurrRoleFSMMgr.CurrRoleCtrl.AStartCurrWayPoint++;
        }

        CurrRoleFSMMgr.CurrRoleCtrl.CharacterController.Move(direction);
       
    }

    /// <summary>
    /// 实现基类 离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToRun.ToString(), false);
    }
}