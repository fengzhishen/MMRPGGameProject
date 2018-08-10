using UnityEngine;
using System.Collections;

/// <summary>
/// NPC������
/// </summary>
public class NPCCtr : MonoBehaviour
{
    /// <summary>
    /// �ǳƹҵ�
    /// </summary>
    [SerializeField]
    private Transform m_HeadBarPos;

    /// <summary>
    /// ͷ��UI��
    /// </summary>
    private GameObject m_HeadBar;

    /// <summary>
    /// ��ǰNPCʵ��
    /// </summary>
    private NPCEntity m_CurrNPCEntity;

    private string[] m_npcTalkContent;

    private NPCHeadBarView m_npcHeadBarView;

    public void Init(NPCWorldMapData npcData,GameObject @object)
    {
        m_CurrNPCEntity = NPCDBModel.GetInstance.GetEntityById(npcData.NPCId);

        InitHeadBar();

        //��ȡnpc��Ҫ˵�Ļ�
        m_npcTalkContent = m_CurrNPCEntity.Talk.Split('|');
    }

    /// <summary>
    /// ��ʼ��NPCͷ��Ѫ��
    /// </summary>
    private void InitHeadBar()
    {
        if (RoleHeadBarRoot.Instance == null) return;

        if (m_CurrNPCEntity == null) return;

        if (m_HeadBarPos == null) return;

        //��ʼ��¡Ԥ��
        m_HeadBar = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIOther, "NPCHeadBar");

        m_HeadBar.transform.parent = RoleHeadBarRoot.Instance.gameObject.transform;

        m_HeadBar.transform.localScale = Vector3.one;

        m_npcHeadBarView = m_HeadBar.GetComponent<NPCHeadBarView>();

        m_npcHeadBarView.Init(m_HeadBarPos, m_CurrNPCEntity.Name);
    }

    /// <summary>
    /// npc���������ʱ����
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
