using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Tutorial Data", fileName = "Tutorial Section")]
public class TutorialData : ScriptableObject
{
    [TextArea(3,6)]
    [SerializeField]
    private string[] text;

    public int SectionLength
    {
        get
        {
            return text.Length;
        }
    }

    public string[] Text
    {
        get
        {
            return text;
        }
    }
}
