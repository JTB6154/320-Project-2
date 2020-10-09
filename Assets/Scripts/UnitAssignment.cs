using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAssignment : MonoBehaviour
{

    #region Fields
    //list of active towers
    //inventory of units
    List<InventoryUnit> inventory = new List<InventoryUnit>();
    [SerializeField] List<GameObject> buttons = new List<GameObject>();
    [SerializeField] Sprite ArcherSprite, WizardSprite, TheurgistSprite, NoneSprite;
    Dictionary<TowerType, Sprite> towerToSprite = new Dictionary<TowerType, Sprite>();
    //reference to currently highlighted unit
    bool unitHighlighted;
	#endregion

	void Start()
    {
        //initialize variables
        GameStats.Instance.Initialize();
        towerToSprite.Add(TowerType.Archer, ArcherSprite);
        towerToSprite.Add(TowerType.Wizard, WizardSprite);
        towerToSprite.Add(TowerType.Theurgist, TheurgistSprite);
        towerToSprite.Add(TowerType.None, NoneSprite);
    }

    void Update()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetActive( inventory[i].Type != TowerType.None);
            buttons[i].GetComponent<SpriteRenderer>().sprite = towerToSprite[inventory[i].Type];
        }

    }

    //we'll give the buttons this function and the proper index
    void displayInventoryItem(int itemIndex)
    { 
        //get some data about the inventory items
        //send it to a lable of some sort
    }


}
