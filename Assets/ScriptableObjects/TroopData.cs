using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Troop", menuName = "New Troop")]
public class TroopData : ScriptableObject
{
    // Name
    public string TroopName;

    // Combat Stats
    public int Cost;
    public float[] AttackSpeed = new float[3];
    public float[] Damage = new float[3];
    public float[] Range = new float[3];
    public TowerType TowerType;

    // Image data and such
    public Sprite ShopPortrait;
    public Sprite InventorySprite;
    public Sprite BattlefieldSprite;

    public override string ToString()
    {
        return "Name: " + TowerName + " Cost: " + Cost + " Range: " + Range + " Damage: " + Damage; 
    }
}
