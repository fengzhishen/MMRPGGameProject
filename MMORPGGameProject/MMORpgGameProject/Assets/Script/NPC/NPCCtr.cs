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

    public void Init(NPCWorldMapData npcData,GameObject @object)
    {
        m_CurrNPCEntity = NPCDBModel.GetInstance.GetEntityById(npcData.NPCId);

        InitHeadBar();
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

        NPCHeadBarView nPCHeadBarView = m_HeadBar.GetComponent<NPCHeadBarView>();

        nPCHeadBarView.Init(m_HeadBarPos, m_CurrNPCEntity.Name);
    }
}
