using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour
{
    public Inventory unitAssignment;
    public UIManager uiManager;
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
            uiManager.towerButtons[i].SetActive(true);
        }

        uiManager.UpdateShopUI(this);
    }

    // Will try to purchase a unit from the shop at the index given
    public void BuyTower(int index)
    {
        if (GameStats.Instance.GetCursorState() == CursorState.BuyingTower) return;

        if (currentShop[index] == TowerType.None)
        {
            return;// false;
        }
        if (!unitAssignment.IsInventoryFull())
        {
            if (GameStats.Instance.PurchaseItemOfType(currentShop[index]))
            {
                // * add it to the inventory *
                TroopPlaceholder newTroop = new TroopPlaceholder(GameStats.Instance.TroopBaseData[currentShop[index]]);
                unitAssignment.AddTroop(newTroop);

                currentShop[index] = TowerType.None;
                uiManager.towerButtons[index].SetActive(false);
                //return true;
            }
        }

    }

    public void UpdateInfoWindow(int index)
    {
        TroopData data = GameStats.Instance.TroopBaseData[currentShop[index]];
        Debug.Log(data.TroopName);
        InfoWindow.UpdateInfoStatic(data);
        
    }
}
