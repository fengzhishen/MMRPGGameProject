using UnityEngine;
using System.Collections;

public class PlayerCtr : SystemBaseCtr<PlayerCtr>
{
    private UIRoleInfoView m_uIRoleInfoView;

    /// <summary>
    /// 更新主城角色信息
    /// </summary>
	public void SetMainCityRoleInfo()
    {
        RoleInfoMainPlayer roleInfoMainPlayer = (RoleInfoMainPlayer)GlobalInit.Instance.CurrPlayer.CurrRoleInfo;

        JobEntity jobEntity = JobDBModel.GetInstance.GetEntityById(roleInfoMainPlayer.JobId);

        string headPic = string.Empty;
        if (jobEntity != null)
        {
            headPic = jobEntity.HeadPic;

            UIMainCityRoleInfoView.Instance.SetUI(headPic, roleInfoMainPlayer.RoleNickName, roleInfoMainPlayer.Level, roleInfoMainPlayer.Money, roleInfoMainPlayer.Gold, roleInfoMainPlayer.CurrHP, roleInfoMainPlayer.CurrMP, roleInfoMainPlayer.MaxHP, roleInfoMainPlayer.MaxMP);
        }
    }

    public void OpenRoleInfoView(Transform container)
    {
        m_uIRoleInfoView = WindowUIMgr.Instance.OpenWindow(WindowUIType.RoleInfo).GetComponent<UIRoleInfoView>();

        m_uIRoleInfoView.gameObject.transform.parent = container;

        m_uIRoleInfoView.transform.localPosition = Vector3.zero;

        m_uIRoleInfoView.transform.localScale = Vector3.one * 0.8f;

        RoleInfoMainPlayer roleInfo = (RoleInfoMainPlayer)GlobalInit.Instance.CurrPlayer.CurrRoleInfo;

        //保存角色相关信息
        TransferData data = new TransferData();
        data.SetValue(ConstDefine.JobId, roleInfo.JobId);
        data.SetValue(ConstDefine.NickName, roleInfo.RoleNickName);
        data.SetValue(ConstDefine.Level, roleInfo.Level);
        data.SetValue(ConstDefine.Fighting, roleInfo.Fighting);
        data.SetValue(ConstDefine.Money, roleInfo.Money);
        data.SetValue(ConstDefine.Gold, roleInfo.Gold);
        data.SetValue(ConstDefine.Exp, roleInfo.Exp);
        data.SetValue(ConstDefine.MaxHP, roleInfo.MaxHP);
        data.SetValue(ConstDefine.MaxMP, roleInfo.MaxMP);
        data.SetValue(ConstDefine.CurrHP, roleInfo.CurrHP);
        data.SetValue(ConstDefine.CurrMP, roleInfo.CurrMP);
        data.SetValue(ConstDefine.Attack, roleInfo.Attack);
        data.SetValue(ConstDefine.Defense, roleInfo.Defense);
        data.SetValue(ConstDefine.Hit, roleInfo.Hit);
        data.SetValue(ConstDefine.Dodge, roleInfo.Dodge);
        data.SetValue(ConstDefine.Cri, roleInfo.Cri);
        data.SetValue(ConstDefine.Res, roleInfo.Res);

        if (m_uIRoleInfoView != null)
        {
            m_uIRoleInfoView.SetRoleInfo(data);
        }
    }
}
