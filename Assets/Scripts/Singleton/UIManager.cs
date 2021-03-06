﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public Slider healthbar;
    public GameObject[] towerButtons;



    // Start is called before the first frame update
    void Start()
    {
        //UpdateShopUI();
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = GameStats.Instance.GetPlayerGold().ToString();

        healthbar.value = GameStats.Instance.GetPlayerHealthPercent();
    }

    public void UpdateShopUI(Shop shop)
    {
        foreach (GameObject go in towerButtons)
        {
            ShopItem shopItemInfo = go.GetComponent<ShopItem>();
            TowerType currentTowerType = shop.currentShop[shopItemInfo.index];
            if (currentTowerType != TowerType.None)
            {
                shopItemInfo.costText.text = GameStats.Instance.GetCost(currentTowerType).ToString();
                shopItemInfo.nameText.text = currentTowerType.ToString();
                shopItemInfo.towerImage.sprite = GameStats.Instance.GetShopPortrait(currentTowerType);
            }
        }
    }

    public void UpdateGoldUI()
    {
        goldText.text = GameStats.Instance.GetPlayerGold().ToString();
    }
}
