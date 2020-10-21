using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialManager : MonoBehaviour
{
    public bool runTutorial = true;
    public List<GameObject> popups;
    public List<TutorialData> sections;

    public int currentSection = 0;
    public int currentSectionPart = 0;

    public void Start()
    {
        for(int i = 0; i< popups.Count; i++)
        {
            popups[i].SetActive(false);
        }
        if (popups != null && runTutorial)
        {
            popups[0].SetActive(true);
            UpdatePopup();
        }
        else
        {
            EndTutorial();
        }
    }

    public void TriggerTutorial(int section)
    {

    }

    // Moves to the next state
    public void AdvanceSectionPart()
    {
        currentSectionPart++;
        if(currentSectionPart >= sections[currentSection].SectionLength)
        {
            currentSectionPart = 0;
            AdvanceSection();
        }
        else
        {
            UpdatePopup();
        }
    }

    private void AdvanceSection()
    {
        currentSection++;
        if(currentSection >= popups.Count)
        {
            EndTutorial();
        }
        else 
        {
            UpdatePopup();
        }
    }

    public void UpdatePopup()
    {
        for (int i = 0; i < popups.Count; i++)
        {
            popups[i].SetActive(false);
        }
        popups[currentSection].SetActive(true);
        TutorialPopup popComp = popups[currentSection].GetComponent<TutorialPopup>();
        popComp.ShowText(sections[currentSection].Text[currentSectionPart]);
    }

    public void EndTutorial()
    {
        for (int i = 0; i < popups.Count; i++)
        {
            popups[i].SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
