using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISelectRoleJobDescView : MonoBehaviour
{
    /// <summary>
    /// 职业名称
    /// </summary>
    [SerializeField]
    private Text m_jobNameText;

    /// <summary>
    /// 职业描述
    /// </summary>
    [SerializeField]
    private Text m_jobDescText;

    public void SetUI(string jobName,string jobDesc)
    {
        m_jobNameText.text = jobName;
        this.m_jobDescText.text = jobDesc;
    }
}
