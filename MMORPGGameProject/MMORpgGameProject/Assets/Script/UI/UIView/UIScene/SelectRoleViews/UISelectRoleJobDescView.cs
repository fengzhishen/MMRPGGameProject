using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISelectRoleJobDescView : MonoBehaviour
{
    /// <summary>
    /// ְҵ����
    /// </summary>
    [SerializeField]
    private Text m_jobNameText;

    /// <summary>
    /// ְҵ����
    /// </summary>
    [SerializeField]
    private Text m_jobDescText;

    public void SetUI(string jobName,string jobDesc)
    {
        m_jobNameText.text = jobName;
        this.m_jobDescText.text = jobDesc;
    }
}
