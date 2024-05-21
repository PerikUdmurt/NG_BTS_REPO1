using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanelUI : MonoBehaviour
{
    public GameObject cardPanel;
    private bool isOpen;
    public void OpenCardPanel()
    {
        if (!isOpen)
        {
            cardPanel.SetActive(true);
            Time.timeScale = 0.1f;
        }
        else if (isOpen) 
        {
            cardPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
