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
            shopItemInfo.costText.text = GameStats.Instance.GetCost(Shop.Instance.currentShop[shopItemInfo.index]).ToString();
            shopItemInfo.nameText.text = Shop.Instance.currentShop[shopItemInfo.index].ToString();
        }
    }

    public void UpdateGoldUI()
    {
        goldText.text = GameStats.Instance.GetPlayerGold().ToString();
    }
}
