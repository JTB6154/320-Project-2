﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    #region Fields
    [Header("Troop Data")]
    //inventory of units
    [SerializeField]
    TroopPlaceholder[] inventory = new TroopPlaceholder[5];
                                  
    //[SerializeField]
    [SerializeField] 
    int maxInventorySize;
    [SerializeField]
    int itemCount = 0;

    [Space]

    [Header("Input")]
    [SerializeField] 
    GameObject buttonParent;//the parent of all of the buttons that make up the inventory

    [Space]

    [Header("Output")]
    [SerializeField] 
    int highlightedIndex;
    public GameObject inventoryGridUnit;
    #endregion

    #region Private Functions
    void Start()
    {
        inventory = new TroopPlaceholder[maxInventorySize];
        highlightedIndex = -1;
    }


    private int GetAvailableSlot()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
    #endregion

    #region Public Functions

    //we'll give the buttons this function and the proper index
    public void DisplayInventoryItem(int itemIndex)
    {
        highlightedIndex = itemIndex;

    }

    /// <summary>
    /// Removes and returns a troop at the given index
    /// </summary>
    /// <returns>the troop at the given index</returns>
    public TroopPlaceholder RemoveAtIndex(int index)
    {
        TroopPlaceholder toReturn = inventory[index];
        inventory[index] = null;
        return toReturn;
    }

    /// <summary>
    /// pops the highlighted troop out
    /// </summary>
    /// <returns>the troop highlighted, if no troop is highlighted returns null</returns>
    public TroopPlaceholder PopHighlightedTroop()
    {
        if (highlightedIndex != -1)
        {
            TroopPlaceholder temp = inventory[highlightedIndex];
            inventory[highlightedIndex] = null; //RemoveAt(highlightedIndex);
                                                //inventory.Add(emptyInvItem);
            itemCount -= 1;
            highlightedIndex = -1;
            return temp;
        }

        return null;
    }

    /// <summary>
    /// Takes in a troop to be added to the inventory
    /// </summary>
    /// <param name="troop">The troop that is sent to the inventory</param>
    /// <returns>if the troop could be added to the inventory</returns>
    public bool AddTroop(TroopPlaceholder troop)
    {
        if (IsInventoryFull())
        {
            return false;
        }

        // Find the next available slot in the inventory
        int newSlot = GetAvailableSlot();
        if (newSlot == -1)
        {
            return false;
        }

        // Add it to Inventory Grid
        GameObject newInventoryItemGO = GameObject.Instantiate(inventoryGridUnit, buttonParent.transform);
        newInventoryItemGO.SetActive(true);
        InventoryUIUnit newInventoryUnit = newInventoryItemGO.GetComponent<InventoryUIUnit>();
        newInventoryUnit.index = newSlot;
        newInventoryUnit.troop = troop;
        newInventoryUnit.UpdateUI();
        InfoWindow.ClearStatic();


        inventory[newSlot] = troop;
        itemCount++;
        return true;
    }

    /// <summary>
    /// Will tell if the inventory is currently full
    /// </summary>
    /// <returns>if the inventory is currently full</returns>
    public bool IsInventoryFull()
    {
        if (itemCount >= maxInventorySize)
        {
            return true;
        }
        return false;
    }


    #endregion

}
