using UnityEngine;
using System.Collections;

/// <summary>
/// NPC控制器
/// </summary>
public class NPCCtr : MonoBehaviour
{
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
    /// 当前NPC实体
    /// </summary>
    private NPCEntity m_CurrNPCEntity;

    public void Init(NPCWorldMapData npcData,GameObject @object)
    {
        m_CurrNPCEntity = NPCDBModel.GetInstance.GetEntityById(npcData.NPCId);

        InitHeadBar(@object);
    }

    /// <summary>
    /// 初始化NPC头顶血条
    /// </summary>
    private void InitHeadBar(GameObject @object)
    {
        if (RoleHeadBarRoot.Instance == null) return;

        if (m_CurrNPCEntity == null) return;

        if (m_HeadBarPos == null) return;

        //开始克隆预设
        m_HeadBar = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIOther, "NPCHeadBar");

        m_HeadBar.transform.parent = RoleHeadBarRoot.Instance.gameObject.transform;

        m_HeadBar.transform.localScale = Vector3.one;

        m_HeadBar.transform.localPosition = Vector3.zero;

        NPCHeadBarView nPCHeadBarView = m_HeadBar.GetComponent<NPCHeadBarView>();

        nPCHeadBarView.Init(@object.transform, m_CurrNPCEntity.Name);
    }
}
