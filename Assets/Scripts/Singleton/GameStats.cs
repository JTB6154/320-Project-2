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

public class GameStats : Singleton<GameStats>
{
	[SerializeField]
	private TowerDataPair[] towerDataPairs;

	public Dictionary<TowerType, TowerData> TowerBaseData = new Dictionary<TowerType, TowerData>();
	int numTotalTowers;
	//public Dictionary<TowerType, int> Costs = new Dictionary<TowerType, int>();
	int playerCash = 19;
	bool hasBeenInitialized = false;

	public override void Initialize()
	{
		//only initialize if we haven't been before
		if (hasBeenInitialized) return;
		//initialize any arrays or dictionaries in the Singleton

		// Initialize the BaseData Dict
		foreach(TowerDataPair tdp in towerDataPairs)
        {
			TowerBaseData.Add(tdp.type, tdp.data);
        }
		numTotalTowers = towerDataPairs.Length;


		//mark that the class has been initialized
		hasBeenInitialized = true;
	}

	public int GetCost(TowerType type)
	{
		return TowerBaseData[type].Cost;
	}

	public Sprite GetShopPortrait(TowerType type)
    {
		return TowerBaseData[type].ShopPortrait;
    }

	public int GetNumTowers()
    {
		return numTotalTowers; 
	}

	public int GetPlayerGold()
    {
		return playerCash;
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
	/// adds the specified amount of gold to the players inventory
	/// </summary>
	/// <param name="amount">the amount of gold to add to the players inventory</param>
	public void AddGold(int amount)
	{
		playerCash += amount;
	}

}