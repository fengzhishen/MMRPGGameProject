using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class UISelectRoleDragView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 m_dragBeginPos = Vector2.zero;
    private Vector2 m_endDragPos = Vector2.zero;

    public Action<int,bool> m_OnSelectRoleDrag;
    private bool m_IsRotating = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_dragBeginPos = eventData.position;
        Debug.Log(m_dragBeginPos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_IsRotating = true;
        Vector2 OnDragPosition = eventData.position;
        float x_pointerVariable = OnDragPosition.x - m_dragBeginPos.x;

        //˵��������һ���
        if (x_pointerVariable > 20)
        {

            if (m_OnSelectRoleDrag != null)
            {
                m_OnSelectRoleDrag(0, m_IsRotating);// 0��ʾ������󻬶�
            }
        }
        //˵��������󻬶�
        else if (x_pointerVariable < -20)
        {
            m_OnSelectRoleDrag(1, m_IsRotating);// 1��ʾ������һ���
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_IsRotating = false;
        m_endDragPos = eventData.position;
        Debug.Log(m_endDragPos);
        if (m_OnSelectRoleDrag != null)
        {
            m_OnSelectRoleDrag(0, m_IsRotating);// 0��ʾ������󻬶�
        }
    }

}
