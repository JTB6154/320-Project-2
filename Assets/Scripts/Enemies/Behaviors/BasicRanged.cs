using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRanged : Enemy
{
    public override void Start()
    {
        base.Start();
        print(Application.dataPath + "/Audio/Enemy/BasicMelee/attack.wav");
        attackNoise = Resources.Load<AudioClip>(Application.dataPath + "/Audio/Enemy/BasicMelee/attack.wav");
        deathNoise = Resources.Load<AudioClip>(Application.dataPath + "/Audio/Enemy/BasicMelee/death.wav");
    }

    /// <summary>
    /// Attacks the player health
    /// </summary>
    public override void Attack()
    {
        // Do math
        base.Attack();
    }

    /// <summary>
    /// Gives the player the value of the unit into their 
    /// </summary>
    public override void Die()
    {
        // Do math
        base.Die();
        print("did this one ma");
    }
}
