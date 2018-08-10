using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIRoleEquipView : MonoBehaviour
{
    [SerializeField]
    private Transform m_roleModelContainer;

    [SerializeField]
    private Text m_lblLevel;

    [SerializeField]
    private Text m_lblFighting;

    [SerializeField]
    private Text m_nickName;

    /// <summary>
    /// 模型职业id
    /// </summary>
    private int m_roleModelJobId;

    void Start()
    {
        CloneRoleModel();
    }
    
    public void SetUI(TransferData data)
    {
        m_roleModelJobId = (int)data.GetValue<byte>(ConstDefine.JobId);
        m_nickName.text = data.GetValue<string>(ConstDefine.NickName); 
        m_lblFighting.text = string.Format("综合战斗力: <color='#FD254DFF'>{0}</color>", data.GetValue<int>(ConstDefine.Fighting));
        m_lblLevel.text = string.Format("LV.{0}", data.GetValue<int>(ConstDefine.Level));
    }
    private void CloneRoleModel()
    {
        GameObject roleModelObj = RoleMgr.Instance.LoadPlayer(m_roleModelJobId);
        roleModelObj.GetComponent<RoleCtrl>().enabled = false;
        roleModelObj.transform.SetParent(m_roleModelContainer);
        roleModelObj.transform.localPosition = Vector3.zero;
        roleModelObj.transform.localScale = Vector3.one;
        roleModelObj.layer = LayerMask.NameToLayer("UI");
        roleModelObj.transform.eulerAngles = new Vector3(0, 180, 0);
        SetRoleModelLayer(roleModelObj,"UI");
    }

    /// <summary>
    /// 递归设置模型的layer
    /// </summary>
    /// <param name="object"></param>
    /// <param name="layerName"></param>
    private void SetRoleModelLayer(GameObject @object,string layerName)
    {
        foreach (Transform item in @object.transform)
        {
            item.gameObject.layer = LayerMask.NameToLayer(layerName);

            if (item.childCount > 0)
            {
                SetRoleModelLayer(item.gameObject, layerName);
            }
            else
            {
                continue;
            }
        }
    }


}

