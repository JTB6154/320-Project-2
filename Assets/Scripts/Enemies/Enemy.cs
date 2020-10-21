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

    //health bar
    public float barDisplay; //current progress
    public Vector2 pos = new Vector2(20, 40);
    public Vector2 size = new Vector2(60, 20);
    public Texture2D emptyTex;
    public Texture2D fullTex;

    public bool IsRanged = false;

    void OnGUI()
    {
        //draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex);
        GUI.EndGroup();
        GUI.EndGroup();
    }

    public void Update()
    {
        pos = transform.position;
        barDisplay = Time.time * 0.05f;
    }

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

        if (CurrentHealth <= 0)
            Die();

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

    /// <summary>
    /// Gives the player the value of the unit into their 
    /// </summary>
    public void Die()
    {
        GameStats.Instance.AddGold(Value);
        gameObject.SetActive(false);
    }
}
