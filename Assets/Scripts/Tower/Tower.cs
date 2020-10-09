using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    TowerData data;
    public int Tier = 1;
    public float AttackSpeed
    {
        get
        {
            return data.AttackSpeed[Tier - 1];
        }
    }
    public float Damage
    {
        get
        {
            return data.Damage[Tier - 1];
        }
    }
    public float Range
    {
        get
        {
            return data.Range[Tier - 1];
        }
    }

    public void SetData(TowerData newData)
    {
        if (data != null) return;

        data = newData;
    }

    public void TierUp()
    {
        if(Tier < 3)
        {
            Tier++;
        }
    }
}
