using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIGameLevelMapPointView : MonoBehaviour
{
    [SerializeField]
    private Image m_pass;

    [SerializeField]
    private Image m_unpass;

    public void SetPoint(bool bIsPass = false)
    {
        m_pass.gameObject.SetActive(bIsPass);
        m_unpass.gameObject.SetActive(!bIsPass);
    }
}
