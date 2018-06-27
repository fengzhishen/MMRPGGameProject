using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UISelectRoleJobItemView : MonoBehaviour
{
    /// <summary>
    /// ְҵ���
    /// </summary>
    [SerializeField]
    private int m_jobId;

    /// <summary>
    /// ��ת��Ŀ��Ƕ�
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
    /// �����ְҵUI�ص�����
    /// </summary>
    private void OnBtnClick()
    {
        if(OnSelectJob != null)
        {
            OnSelectJob(m_jobId, m_rotateAngle,this);
        }
    }
  
}
