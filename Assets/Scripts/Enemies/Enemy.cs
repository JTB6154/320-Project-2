using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores the data of Enemies
/// </summary>
public class Enemy : MonoBehaviour
{
    public float MaxHealth = 100;
    public float CurrentHealth;
    public int Damage = 1;
    public float AttackSpeed = 0.5f;

    public bool IsRanged = false;

    public void Start()
    {
        CurrentHealth = MaxHealth;
        gameObject.GetComponent<FollowPath>().IsRanged = IsRanged;
    }

    /// <summary>
    /// Subtracts a value of damage from the enemy's health
    /// </summary>
    /// <param name="deltaHealth">The amount of damage to take</param>
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    /// <summary>
    /// Adds a value of healing to the enemy's health
    /// </summary>
    /// <param name="healing">The amount of healing to add</param>
    public void HealHealth(float healing)
    {
        CurrentHealth += healing;

        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
    }

    /// <summary>
    /// Attacks the player health
    /// </summary>
    public void Attack()
    {
        GameStats.Instance.SubtractHealth(Damage);
        print(GameStats.Instance.GetPlayerHealth());
        // Do animation too
    }
}
