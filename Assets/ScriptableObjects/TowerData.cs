using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "New Tower")]
public class TowerData : ScriptableObject
{
    // Name
    public string TowerName;

    // Combat Stats
    public int Cost;
    public float[] AttackSpeed = new float[3];
    public float[] Damage = new float[3];
    public float[] Range = new float[3];

    // Image data and such
    public Sprite ShopPortrait;
}
