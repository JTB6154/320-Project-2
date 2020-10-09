using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public int index;
    public Image towerImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;

    public void BuyItem()
    {
        Shop.Instance.BuyTower(index);
    }
}
