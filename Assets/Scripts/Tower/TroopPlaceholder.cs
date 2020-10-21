using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopPlaceholder
{
    public int tier = 1;
    public TroopData data;

    public TroopPlaceholder(TroopData data)
    {
        this.data = data;
    }

    public int MaxTier
    {
        get
        {
            return data.MaxTier;
        }
    }
    public string TroopName
    {
        get
        {
            return data.TroopName;
        }
    }
    public string Description
    {
        get
        {
            return data.Description;
        }
    }
    public float AttackSpeed
    {
        get
        {
            return data.AttackSpeed[tier - 1];
        }
    }
    public float Damage
    {
        get
        {
            return data.Damage[tier - 1];
        }
    }
    public float Range
    {
        get
        {
            return data.Range[tier - 1] / 50;
        }
    }
    public int Cost
    {
        get
        {
            return data.Cost;
        }
    }

    public Sprite PortraitSprite
    {
        get
        {
            return data.ShopPortrait;
        }
    }

    public Sprite InventorySprite
    {
        get
        {
            return data.InventorySprite;
        }
    }

    public Sprite BattlefieldSprite
    {
        get
        {
            return data.BattlefieldSprite;
        }
    }
    public void SetData(TroopData newData)
    {
        if (data != null) return;

        data = newData;
    }
    public void TierUp()
    {
        if (tier < 3)
        {
            tier++;
        }
    }

    public override string ToString()
    {
        return data.ToString();
    }
}
