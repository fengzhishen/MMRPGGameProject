using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UISelectRoleJobItemView : MonoBehaviour
{
    /// <summary>
    /// 职业编号
    /// </summary>
    [SerializeField]
    private int m_jobId;

    /// <summary>
    /// 旋转的目标角度
    /// </summary>
    [SerializeField]
    private int m_rotateAngle;

   // public delegate void OnSelectJobHandler(int jobId,int rotateAngle);

    public Action<int, int, UISelectRoleJobItemView> OnSelectJob;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Button>().onClick.AddListener(OnBtnClick);
	}

    /// <summary>
    /// 当点击职业UI回调处理
    /// </summary>
    private void OnBtnClick()
    {
        if(OnSelectJob != null)
        {
            OnSelectJob(m_jobId, m_rotateAngle,this);
        }
    }
  
}
