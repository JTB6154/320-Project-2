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
    [SerializeField] int maxInventorySize;
        int itemCount = 0;
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
            //maxInventorySize = buttons.Length;
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

        /// <summary>
        /// pops the highlighted troop out
        /// </summary>
        /// <returns>the troop highlighted, if no troop is highlighted returns null</returns>
        public TroopPlaceholder PopHighlightedTroop()
        {
            if (highlightedIndex != -1)
            {
                TroopPlaceholder temp = inventory[highlightedIndex];
                inventory.RemoveAt(highlightedIndex);
                inventory.Add(emptyInvItem);
                itemCount -= 1;
                outputLabel.text = "";
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
            if (itemCount + 1 > maxInventorySize)
            {
                return false;
            }

            itemCount++;
            inventory[itemCount] = troop;
            return true;
        }
	#endregion

}
