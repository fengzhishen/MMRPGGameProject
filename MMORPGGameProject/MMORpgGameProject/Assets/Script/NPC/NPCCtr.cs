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

    private string[] m_npcTalkContent;

    private NPCHeadBarView m_npcHeadBarView;

    public void Init(NPCWorldMapData npcData,GameObject @object)
    {
        m_CurrNPCEntity = NPCDBModel.GetInstance.GetEntityById(npcData.NPCId);

        InitHeadBar();

        //提取npc需要说的话
        m_npcTalkContent = m_CurrNPCEntity.Talk.Split('|');
    }

    /// <summary>
    /// 初始化NPC头顶血条
    /// </summary>
    private void InitHeadBar()
    {
        if (RoleHeadBarRoot.Instance == null) return;

        if (m_CurrNPCEntity == null) return;

        if (m_HeadBarPos == null) return;

        //开始克隆预设
        m_HeadBar = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIOther, "NPCHeadBar");

        m_HeadBar.transform.parent = RoleHeadBarRoot.Instance.gameObject.transform;

        m_HeadBar.transform.localScale = Vector3.one;

        m_npcHeadBarView = m_HeadBar.GetComponent<NPCHeadBarView>();

        m_npcHeadBarView.Init(m_HeadBarPos, m_CurrNPCEntity.Name);
    }

    /// <summary>
    /// npc自言自语的时间间隔
    /// </summary>
    private float m_nextTalkTime = 0;

    private void Update()
    {
        if(Time.time > m_nextTalkTime)
        {
            m_nextTalkTime = Time.time + 10;

            if(m_npcHeadBarView !=null)
            {
                m_npcHeadBarView.NPCTalck(m_npcTalkContent[UnityEngine.Random.Range(0, m_npcTalkContent.Length - 1)], 2f);
            }
        }   
    }
}
