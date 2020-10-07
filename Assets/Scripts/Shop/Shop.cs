using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Singleton<Shop>
{
    public TowerType[] currentShop;

    public void Start()
    {
        Debug.Log("Shop Start Begin");
        currentShop = new TowerType[5];
        RefreshShop();
        Debug.Log("Shop Start Done");
    }

    // Puts 5 new towers in the shop, and tell the UI to update
    public void RefreshShop()
    {
        // Basic random refresh
        int numTowers = GameStats.Instance.GetNumTowers();
        // Get a random tower, 5 times
        for (int i = 0; i < 5; i++)
        {
            TowerType newTower = (TowerType)Random.Range(1, numTowers + 1);
            currentShop[i] = newTower;
            // Enable all the buttons
            UIManager.Instance.towerButtons[i].SetActive(true);
        }

        UIManager.Instance.UpdateShopUI();
    }

    // Will try to purchase a unit from the shop at the index given
    public bool BuyTower(int index)
    {
        if(currentShop[index] == TowerType.None)
        {
            return false;
        }
        if(GameStats.Instance.PurchaseItemOfType(currentShop[index]))
        {
            currentShop[index] = TowerType.None;
            UIManager.Instance.towerButtons[index].SetActive(false);
            return true;
        }
        else
        {
            return false;
        }
    }
}
