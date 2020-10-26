using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject uiPanel;

    public void ActivateMenu()
    {
        uiPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void DeactivateMenu()
    {
        uiPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

}
