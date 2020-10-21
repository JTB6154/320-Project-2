using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    //[TextArea(3,6)]
    //public string tutorialText= bungus;

    [SerializeField]
    private GameObject background;
    [SerializeField]
    private GameObject text;

    private void Awake()
    {
        background = gameObject.transform.Find("Pop up").gameObject;
        text = background.transform.Find("Text").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        TextMeshProUGUI textPro = text.GetComponent<TextMeshProUGUI>();
        RectTransform backgroundRect = background.GetComponent<RectTransform>();
        textPro.text = tutorialText;
        float newWidth = Mathf.Min(500f, textPro.preferredWidth);
        float newHeight = Mathf.Min(500f, textPro.preferredHeight);

        backgroundRect.sizeDelta = new Vector2(backgroundRect.rect.width, textPro.preferredHeight + 10f);
        */
        //backgroundRect.rect.Set(backgroundRect.rect.x, backgroundRect.rect.y, textPro.preferredWidth, textPro.preferredHeight);
    }

    public void ShowText(string newText)
    {
        TextMeshProUGUI textPro = text.GetComponent<TextMeshProUGUI>();
        RectTransform backgroundRect = background.GetComponent<RectTransform>();
        textPro.text = newText;
        float newWidth = Mathf.Min(500f, textPro.preferredWidth);
        float newHeight = Mathf.Min(500f, textPro.preferredHeight);

        backgroundRect.sizeDelta = new Vector2(backgroundRect.rect.width, textPro.preferredHeight + 10f);
    }
}
