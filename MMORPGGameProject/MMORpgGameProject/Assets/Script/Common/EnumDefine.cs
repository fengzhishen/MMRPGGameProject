//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-01 21:51:56
//备    注：
//===================================================
using UnityEngine;
using System.Collections;

#region SceneType 场景类型
/// <summary>
/// 场景类型
/// </summary>
public enum SceneType
{
    LogOn,

    WorldMap,

    SelectRole
}
#endregion

#region WindowUIType 窗口类型
/// <summary>
/// 窗口类型
/// </summary>
public enum WindowUIType
{
    /// <summary>
    /// 未设置
    /// </summary>
    None,
    /// <summary>
    /// 登录窗口
    /// </summary>
    LogOn,
    /// <summary>
    /// 注册窗口
    /// </summary>
    Reg,
    /// <summary>
    /// 角色信息窗口
    /// </summary>
    RoleInfo,

    /// <summary>
    /// 进入区服
    /// </summary>
    GameServerEnter,

    /// <summary>
    /// 区服选择
    /// </summary>
    GameServerSelect,

    /// <summary>
    /// 剧情关卡地图
    /// </summary>
    GameLevelMap,

    /// <summary>
    /// 剧情关卡详情
    /// </summary>
    GameLevelDetail
}
#endregion

#region WindowUIContainerType UI容器类型
/// <summary>
/// UI容器类型
/// </summary>
public enum WindowUIContainerType
{
    /// <summary>
    /// 左上
    /// </summary>
    TopLeft,
    /// <summary>
    /// 右上
    /// </summary>
    TopRight,
    /// <summary>
    /// 左下
    /// </summary>
    BottomLeft,
    /// <summary>
    /// 右下
    /// </summary>
    BottomRight,
    /// <summary>
    /// 居中
    /// </summary>
    Center
}
#endregion

#region WindowShowStyle 窗口打开方式
/// <summary>
/// 窗口打开方式
/// </summary>
public enum WindowShowStyle
{
    /// <summary>
    /// 正常打开
    /// </summary>
    Normal,
    /// <summary>
    /// 从中间放大
    /// </summary>
    CenterToBig,
    /// <summary>
    /// 从上往下
    /// </summary>
    FromTop,
    /// <summary>
    /// 从下往上
    /// </summary>
    FromDown,
    /// <summary>
    /// 从左向右
    /// </summary>
    FromLeft,
    /// <summary>
    /// 从右向左
    /// </summary>
    FromRight
}
#endregion

#region RoleType 角色类型
/// <summary>
/// 角色类型
/// </summary>
public enum RoleType
{
    /// <summary>
    /// 未设置
    /// </summary>
    None = 0,
    /// <summary>
    /// 当前玩家
    /// </summary>
    MainPlayer = 1,
    /// <summary>
    /// 怪
    /// </summary>
    Monster = 2
}
#endregion

/// <summary>
/// 角色状态
/// </summary>
public enum RoleState
{
    /// <summary>
    /// 未设置
    /// </summary>
    None = 0,
    /// <summary>
    /// 待机
    /// </summary>
    Idle = 1,
    /// <summary>
    /// 跑了
    /// </summary>
    Run = 2,
    /// <summary>
    /// 攻击
    /// </summary>
    Attack = 3,
    /// <summary>
    /// 受伤
    /// </summary>
    Hurt = 4,
    /// <summary>
    /// 死亡
    /// </summary>
    Die = 5
}

/// <summary>
/// 角色动画状态名称
/// </summary>
public enum RoleAnimatorName
{
    Idle_Normal,
    Idle_Fight,
    Run,
    Hurt,
    Die,
    PhyAttack1,
    PhyAttack2,
    PhyAttack3
}

public enum ToAnimatorCondition
{
    ToIdleNormal,
    ToIdleFight,
    ToRun,
    ToHurt,
    ToDie,
    ToPhyAttack,
    CurrState
}

/// <summary>
/// 消息类型
/// </summary>
public enum MessageViewType
{
    Ok,
    OkAndCancel
}

/// <summary>
/// 语言
/// </summary>
public enum Language
{
    CN,
    EN
}

/// <summary>
/// 图片资源类型
/// </summary>
public enum SpriteSourceType
{
    /// <summary>
    /// 剧情关卡图标
    /// </summary>
    GameLevelIco = 0,
    /// <summary>
    /// 剧情关卡详情图片
    /// </summary>
    GameLevelDetail = 1,
    /// <summary>
    /// 世界地图图标
    /// </summary>
    WorldMapIco = 2,
    /// <summary>
    /// 世界地图小地图
    /// </summary>
    WorldMapSmall = 3
}

/// <summary>
/// 角色动画状态
/// </summary>
public enum RoleAnimatorState
{
    Idle_Normal = 1,
    Idle_Fight = 2,
    Run = 3,
    Hurt = 4,
    Die = 5,
    Select = 6,
    XiuXian = 7,
    Died = 8,
    PhyAttack1 = 11,
    PhyAttack2 = 12,
    PhyAttack3 = 13,
    Skill1 = 14,
    Skill2 = 15,
    Skill3 = 16,
    Skill4 = 17,
    Skill5 = 18,
    Skill6 = 19,
}
/// <summary>
/// 角色攻击类型
/// </summary>
public enum RoleAttackType
{
    /// <summary>
    /// 物理攻击
    /// </summary>
    PhyAttack,
    /// <summary>
    /// 技能攻击
    /// </summary>
    SkillAttack
}

/// <summary>
/// 游戏关卡难度等级
/// </summary>
public enum GameLevelGrade
{
    /// <summary>
    /// 普通
    /// </summary>
    Normal = 0,
    /// <summary>
    /// 困难
    /// </summary>
    Hard = 1,
    /// <summary>
    /// 地狱
    /// </summary>
    Hell = 2
}

/// <summary>
/// 物品类型
/// </summary>
public enum GoodsType
{
    /// <summary>
    /// 装备
    /// </summary>
    Equip = 0,
    /// <summary>
    /// 道具
    /// </summary>
    Item = 1,
    /// <summary>
    /// 材料
    /// </summary>
    Material = 2
}
