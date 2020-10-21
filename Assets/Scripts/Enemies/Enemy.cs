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
    public int Value = 5;

    public bool IsRanged = false;
    public EnemySpawnManager manager;
    [SerializeField] protected AudioSource source;
    [SerializeField] protected AudioClip attackNoise;
    [SerializeField] protected AudioClip deathNoise;

    public virtual void Start()
    {
        CurrentHealth = MaxHealth;
        gameObject.GetComponent<FollowPath>().IsRanged = IsRanged;
        source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Subtracts a value of damage from the enemy's health
    /// </summary>
    /// <param name="deltaHealth">The amount of damage to take</param>
    public virtual void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
            Die();

    }

    /// <summary>
    /// Adds a value of healing to the enemy's health
    /// </summary>
    /// <param name="healing">The amount of healing to add</param>
    public virtual void HealHealth(float healing)
    {
        CurrentHealth += healing;

        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
    }

    /// <summary>
    /// Attacks the player health
    /// </summary>
    public virtual void Attack()
    {
        GameStats.Instance.SubtractHealth(Damage);
        print(GameStats.Instance.GetPlayerHealth());

        // Play audio
        manager.playSound(attackNoise, 0.25f);

        // Do animation too
    }

    /// <summary>
    /// Gives the player the value of the unit into their 
    /// </summary>
    public virtual void Die()
    {
        GameStats.Instance.AddGold(Value);

        // Play audio
        manager.playSound(deathNoise, 1.0f);

        gameObject.SetActive(false);
    }
}
