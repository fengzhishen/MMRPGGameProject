using UnityEngine;
using System.Collections;

public class UIRoleInfoView : UIWindowViewBase
{
    [SerializeField]
    private UIRoleEquipView m_roleEquipView;

    [SerializeField]
    private UIRoleInfoDetailView m_roleInfoDetailView;

    public void SetRoleInfo(TransferData data)
    {
        m_roleEquipView.SetUI(data);
        m_roleInfoDetailView.SetUI(data);
    }
}
