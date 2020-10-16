using System.Collections;
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

	public Dictionary<TowerType, TroopData> TroopBaseData = new Dictionary<TowerType, TroopData>();
	int numTotalTowers;
	//public Dictionary<TowerType, int> Costs = new Dictionary<TowerType, int>();
	int playerCash = 100;
	int playerMaxHealth = 100;
	int playerHealth;
	bool hasBeenInitialized = false;
	CursorState state;



	public override void Initialize()
	{
		//only initialize if we haven't been before
		if (hasBeenInitialized) return;
		//initialize any arrays or dictionaries in the Singleton

		// Initialize the BaseData Dict
		foreach(TowerDataPair tdp in towerDataPairs)
        {
			TroopBaseData.Add(tdp.type, tdp.data);
        }
		numTotalTowers = towerDataPairs.Length;

		//set player health to the players max health
		playerHealth = playerMaxHealth;


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

	public CursorState GetCursorState()
	{ 
		return state; 
	}

	public void SetCursorState(CursorState cState)
	{
		state = cState;
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