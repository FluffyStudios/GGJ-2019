using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Button m_accuseBtn;
    [SerializeField] private Button m_cancelAccuseBtn;
    [SerializeField] private Button m_validateAccuseBtn;
    [SerializeField] private GameObject m_accusationFilter;
    // Start is called before the first frame update
    void Start()
    {
        m_cancelAccuseBtn.gameObject.SetActive(false);
        m_validateAccuseBtn.gameObject.SetActive(false);
        m_accusationFilter.SetActive(false);
    }
    
    public void OnAccuseBtnClick()
    {
        m_cancelAccuseBtn.gameObject.SetActive(true);
        m_validateAccuseBtn.gameObject.SetActive(true);
        m_accuseBtn.gameObject.SetActive(false);
        m_accusationFilter.SetActive(true);
        PlanetManager.Instance.StartAccusation();
    }
    public void OnAccuseCanceled()
    {
        m_cancelAccuseBtn.gameObject.SetActive(false);
        m_validateAccuseBtn.gameObject.SetActive(false);
        m_accuseBtn.gameObject.SetActive(true);
        m_accusationFilter.SetActive(false);
        PlanetManager.Instance.CancelAccusation();
    }

    public void OnAccuseValidated()
    {
        m_cancelAccuseBtn.gameObject.SetActive(false);
        m_validateAccuseBtn.gameObject.SetActive(false);
        m_accuseBtn.gameObject.SetActive(false);
        m_accusationFilter.SetActive(false);
        PlanetManager.Instance.ResolveAccusation();
    }
}
