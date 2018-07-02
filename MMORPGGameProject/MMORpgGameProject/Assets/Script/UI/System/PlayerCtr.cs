using UnityEngine;
using System.Collections;

public class PlayerCtr : SystemBaseCtr<PlayerCtr>
{
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
}
