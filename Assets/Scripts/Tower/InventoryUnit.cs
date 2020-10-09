using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUnit : MonoBehaviour
{
    #region Fields
    [SerializeField] TowerType type = TowerType.None;
    public TowerType Type
    {
        get { return type; }
    }
    [Space]
    int level = -1; //initialize to negative one so we can call level up to initialize
    [SerializeField] float baseRange;
    [SerializeField] float rangePerLevel;
    [Space]
    [SerializeField] int baseDamage;
    [SerializeField] int damagePerLevel;
    [Space]
    [SerializeField] float baseAttackSpeed;
    [SerializeField] float attackSpeedPerLevel;

    float range, attackSpeed;
    int damage;

    #endregion
    void Start()
    {
        //initializeto level one
        levelUp();
    }

    public void Highlight()
    { 
        
    }

    void levelUp()
    {
        level++;
        range = updateStatLevel(baseRange, rangePerLevel);
        damage = updateStatLevel(baseDamage, damagePerLevel);
        attackSpeed = updateStatLevel(baseAttackSpeed, attackSpeedPerLevel);
    }

    int updateStatLevel(int baseStat, int incrementPerLevel)
    {
        return baseStat + incrementPerLevel * level;
    }

    float updateStatLevel(float baseStat, float incrementPerLevel)
    {
        return baseStat + incrementPerLevel * level;
    }



}
