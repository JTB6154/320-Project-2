using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI goldText;
    public GameObject[] towerButtons;



    // Start is called before the first frame update
    void Start()
    {
        UpdateShopUI();
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = GameStats.Instance.GetPlayerGold().ToString();
    }

    public void UpdateShopUI()
    {
        foreach(GameObject go in towerButtons)
        {
            ShopItem shopItemInfo = go.GetComponentInChildren<ShopItem>();
            TowerType currentTowerType = Shop.Instance.currentShop[shopItemInfo.index];
            shopItemInfo.costText.text = GameStats.Instance.GetCost(currentTowerType).ToString();
            shopItemInfo.nameText.text = currentTowerType.ToString();
            shopItemInfo.towerImage.sprite = GameStats.Instance.GetShopPortrait(currentTowerType);
        }
    }

    public void UpdateGoldUI()
    {
        goldText.text = GameStats.Instance.GetPlayerGold().ToString();
    }
}
