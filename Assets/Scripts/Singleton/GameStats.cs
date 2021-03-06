﻿using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public enum TowerType { 
	None, // For if there should be null
	Archer,
	Wizard,
	Theurgist
}

public enum CursorState { 
	Free,
	BuyingTower
}

public class GameStats : Singleton<GameStats>
{
	[SerializeField]
	private TowerDataPair[] towerDataPairs;

	public bool runTutorial = true;
	public Dictionary<TowerType, TroopData> TroopBaseData = new Dictionary<TowerType, TroopData>();
	int numTotalTowers;
	//public Dictionary<TowerType, int> Costs = new Dictionary<TowerType, int>();
	int playerCash = 100;
	int playerMaxHealth = 100;
	int playerHealth;
	bool hasBeenInitialized = false;
	CursorState state;
	int towerBaseCost = 10;
	int towerCost;
	int towerCostIncrease = 5;
	int numTowersPurchased = 0;



	public override void Initialize()
	{
		//only initialize if we haven't been before
		if (hasBeenInitialized) return;
		//initialize any arrays or dictionaries in the Singleton
		TroopBaseData = new Dictionary<TowerType, TroopData>();
		// Initialize the BaseData Dict
		foreach(TowerDataPair tdp in towerDataPairs)
        {
			TroopBaseData.Add(tdp.type, tdp.data);
        }
		numTotalTowers = towerDataPairs.Length;

		numTowersPurchased = 0;
		playerCash = 100;
		towerCostIncrease = 5;

		//set player health to the players max health
		playerHealth = playerMaxHealth;

		towerCost = towerBaseCost;

		//mark that the class has been initialized
		hasBeenInitialized = true;
	}

	public int GetCost(TowerType type)
	{
		return TroopBaseData[type].Cost;
	}

	public Sprite GetShopPortrait(TowerType type)
    {
		return TroopBaseData[type].ShopPortrait;
    }

	public int GetNumTowers()
    {
		return numTotalTowers; 
	}

	public int GetPlayerGold()
    {
		return playerCash;
    }

	public int GetPlayerHealth()
	{
		return playerHealth;
	}

	public float GetPlayerHealthPercent()
    {
		return (float)playerHealth / (float)playerMaxHealth;
    }

	public CursorState GetCursorState()
	{ 
		return state; 
	}

	public void SetCursorState(CursorState cState)
	{
		state = cState;
	}

	public int GetCurrentTowerCost()
	{
		return towerCost;
	}

	/// <summary>
	/// Returns wether or not you can purchase the tower of given type and if you can removes the gold
	/// </summary>
	/// <param name="type">The type of tower you are attempting to purchase</param>
	/// <returns>returns true if the tower was successfully purchased, and false if it was not</returns>
	public bool PurchaseItemOfType(TowerType type)
	{
		if (GetCost(type) > playerCash)
		{
			return false;
		}
		else
		{
			playerCash -= GetCost(type);
			return true;
		}
	}

	public bool PurchaseTower()
	{
		if (towerCost > playerCash)
			return false;
		else{
			playerCash -= towerCost;
			numTowersPurchased++;
			towerCost += towerCostIncrease * numTowersPurchased;
			return true;
		}
	}
	/// <summary>
	/// Returns true if the player has enough cash to refresh the shop, and removes the gold
	/// </summary>
	/// <param name="cost"/>The cost of refreshing the shop
	/// <returns>returns true if the shop refresh could be and was purchases</returns>
	public bool PurchaseRefresh(int cost)
    {
		if (cost > playerCash)
		{
			return false;
		}
		else
		{
			playerCash -= cost;
			return true;
		}
	}

	/// <summary>
	/// adds the specified amount of gold to the players inventory
	/// </summary>
	/// <param name="amount">the amount of gold to add to the players inventory</param>
	public void AddGold(int amount)
	{
		playerCash += amount;
	}

	public bool SubtractHealth(int amount)
	{
		playerHealth -= amount;
		return (playerHealth >= 0);
	}

	public void ReInitialize()
	{
		hasBeenInitialized = false;
		Initialize();
	}

}