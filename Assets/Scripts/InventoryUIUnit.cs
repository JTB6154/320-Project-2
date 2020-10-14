using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIUnit : MonoBehaviour
{
    public Inventory inventory;
    public int index = 0;
    public TroopPlaceholder troop;
    public Button button;
    public Image image;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI tierText;
    public Image highlightTint;
    public bool isHighlighted = false;
    //public Color highlightedColor;

    public void SetTroop(TroopPlaceholder troop)
    {
        this.troop = troop;
    }
    public void UpdateUI()
    {
        image.sprite = troop.InventorySprite;
        nameText.text = troop.TroopName;
        tierText.text = troop.tier.ToString();
        highlightTint.gameObject.SetActive(isHighlighted);
    }

    public void UpdateInfoWindow()
    {
        InfoWindow.UpdateInfoStatic(troop);
    }
    
    public void SetHighlightedUnit()
    {
        inventory.SetHighlightedUnit(index);
    }



}
