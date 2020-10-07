using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Singleton<Shop>
{
    public TowerType[] currentShop;


    // Puts 5 new towers in the shop
    public void RefreshShop()
    {
        // Basic random refresh
        int numTowers = GameStats.Instance.GetNumTowers();
        // Get a random tower, 5 times
        for (int i = 0; i < 5; i++)
        {
            TowerType newTower = (TowerType)Random.Range(0, numTowers - 1);
            currentShop[i] = newTower;
        }
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
            return true;
        }
        else
        {
            return false;
        }
    }
}
