using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : MonoBehaviour
{
    private static InfoWindow instance;

    public Image portrait;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI tierText;
    // Stats
    public TextMeshProUGUI rangeValueText;
    public TextMeshProUGUI damageValueText;
    public TextMeshProUGUI asValueText;

    private void Awake()
    {
        instance = this;
    }

    
    public void Start()
    {
        gameObject.SetActive(false);
    }
    

    public void UpdateInfo(TroopPlaceholder troop)
    {
        gameObject.SetActive(true);
        portrait.sprite = troop.InventorySprite;
        nameText.text = troop.TroopName;
        descriptionText.text = troop.Description;
        tierText.text = troop.tier.ToString();
        rangeValueText.text = troop.Range.ToString();
        damageValueText.text = troop.Damage.ToString();
        asValueText.text = troop.AttackSpeed.ToString();

    }

    public void Clear()
    {
        gameObject.SetActive(false);
    }

    public static void ClearStatic()
    {
        instance.Clear();
    }

    public static void UpdateInfoStatic(TroopData data)
    {
        instance.UpdateInfo(new TroopPlaceholder(data));
    }

    public static void UpdateInfoStatic(TroopPlaceholder troop)
    {
        instance.UpdateInfo(troop);
    }



}
