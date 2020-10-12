using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitAssignment : MonoBehaviour
{

    #region Fields
    [Header("Troop Data")]
        //inventory of units
        List<TroopPlaceholder> inventory = new List<TroopPlaceholder>();
        //troop empty units
        TroopPlaceholder emptyInvItem = new TroopPlaceholder();
        //data of an empty unit
        [SerializeField] TroopData EmptyInvItem;
    [Space]
    [Header("Input")]
        [SerializeField] GameObject buttonParent;//the parent of all of the buttons that make up the inventory
        Button[] buttons;
    [Space]
    [Header("Output")]
        [SerializeField] Text outputLabel;
        int highlightedIndex;
	#endregion

	#region UnityFunctions
	    void Start()
        {
            emptyInvItem.SetData(EmptyInvItem);
            highlightedIndex = -1;
            buttons = buttonParent.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; i++)
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
                buttons[i].enabled = inventory[i].data.TowerType != TowerType.None;
                buttons[i].GetComponent<Image>().sprite = inventory[i].PortraitSprite;
            }

        }
	#endregion

	#region Public Functions

	    //we'll give the buttons this function and the proper index
	    public void DisplayInventoryItem(int itemIndex)
        {
            //get some data about the inventory items
            highlightedIndex = itemIndex;

            //send it to a lable of some sort
            outputLabel.text = inventory[itemIndex].ToString();
        }

        public TroopPlaceholder PopHighlightedTroop()
        {
            TroopPlaceholder temp = inventory[highlightedIndex];
            inventory[highlightedIndex] = emptyInvItem;
            return temp;
        }
	#endregion

}
