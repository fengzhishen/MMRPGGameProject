using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UIRoleInfoDragView : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Transform m_dragTargetParent;

    private Transform m_dragTarget;

    private Vector3 m_beginDragPos;

    private Vector3 m_endDragPos;

    private Vector3 m_beginDragTargetPos;
    float x;

    private bool m_isDraging = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(m_dragTargetParent.childCount > 0)
        {
            m_dragTarget = m_dragTargetParent.GetChild(0);
        }
        m_isDraging = true;
        m_beginDragPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.IsPointerMoving())
        {
            m_isDraging = true;
        }
        else
        {
            m_isDraging = false;
        }

        m_endDragPos = eventData.position;
        x = m_endDragPos.x - m_beginDragPos.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_isDraging = false;
        m_endDragPos = eventData.position;
    }

    void Update()
    {     
        if(m_isDraging == true)
            m_dragTarget.transform.eulerAngles = Vector3.Lerp(m_dragTarget.transform.eulerAngles, m_dragTarget.transform.eulerAngles + new Vector3(0, Time.deltaTime * ((x > 0?-1:1)) * 500, 0), 0.5f);
    }
}
