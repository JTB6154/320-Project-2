using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitAssignment : MonoBehaviour
{

    #region Fields
    //list of active towers
    //inventory of units
    List<TowerData> inventory = new List<TowerData>();
    [SerializeField] TowerData inv1Test;
    [SerializeField] TowerData emptyInvItem;
    [SerializeField] GameObject buttonParent;
    [SerializeField] Text outputLabel;
    int highlightedIndex;
    Button[] buttons;
    //reference to currently highlighted unit
    bool unitHighlighted;
	#endregion

	void Start()
    {
        highlightedIndex = -1;
        buttons = buttonParent.GetComponentsInChildren<Button>();
        inventory.Add(inv1Test);
        for (int i = 1; i < buttons.Length; i++)
        {
            inventory.Add(emptyInvItem);
        }
    }

    void Update()
    {
        //Debug.Log(buttons);
        for (int i = 0; i < buttons.Length; i++)
        {
            Debug.Log(buttons[i].name);
            buttons[i].enabled =  inventory[i].TowerType != TowerType.None;
            buttons[i].GetComponent<Image>().sprite = inventory[i].InventorySprite;
        }

    }

    //we'll give the buttons this function and the proper index
    public void displayInventoryItem(int itemIndex)
    {
        //get some data about the inventory items
        highlightedIndex = itemIndex;
        
        //send it to a lable of some sort
        outputLabel.text = inventory[itemIndex].ToString();
    }


}
