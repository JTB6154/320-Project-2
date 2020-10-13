using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIUnit : MonoBehaviour
{
    public int index = 0;
    public TroopPlaceholder troop;
    public Image image;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI tierText;
    public Inventory unitAssignment;

    public void SetTroop(TroopPlaceholder troop)
    {
        this.troop = troop;
    }
    public void UpdateUI()
    {
        image.sprite = troop.InventorySprite;
        nameText.text = troop.TroopName;
        tierText.text = troop.tier.ToString();
    }

    public void UpdateInfoWindow()
    {
        InfoWindow.UpdateInfoStatic(troop);
    }



}
