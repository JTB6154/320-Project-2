using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopPlaceholder : MonoBehaviour
{
    public int tier;
    public Sprite sprite;
    public TowerData data;
    public float[] attackSpeed;
    public int cost;
    public float[] damage;
    // Start is called before the first frame update
    void Start()
    {
        data = new TowerData();
        data.BattlefieldSprite = this.sprite;
        data.AttackSpeed = this.attackSpeed;
        data.Cost = this.cost;
        data.Damage = this.damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
