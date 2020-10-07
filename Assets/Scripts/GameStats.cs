using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType { 
	None, // For if there should be null
	Archer,
	Wizard,
	Theurgist
}

public class GameStats : Singleton<GameStats>
{
	Dictionary<TowerType, int> Costs = new Dictionary<TowerType, int>();
	int playerCash = 19;
	bool hasBeenInitialized = false;

	public void Initialize()
	{
		//only initialize if we haven't been before
		if (hasBeenInitialized) return;
		//initialize any arrays or dictionaries in the Singleton

		//initialize the costs dictionary
		Costs[TowerType.Archer] = 10;
		Costs[TowerType.Wizard] = 10;
		Costs[TowerType.Theurgist] = 10;


		//mark that the class has been initialized
		hasBeenInitialized = true;
	}

	public int GetCost(TowerType type)
	{
		return Costs[type];
	}

	public int GetNumTowers()
    {
		return System.Enum.GetNames(typeof(TowerType)).Length - 1; 

	}

	/// <summary>
	/// Returns wether or not you can purchase the tower of given type and if you can removes the gold
	/// </summary>
	/// <param name="type">The type of tower you are attempting to purchase</param>
	/// <returns>returns true if the tower was successfully purchased, and false if it was not</returns>
	public bool PurchaseItemOfType(TowerType type)
	{
		if (Costs[type] > playerCash)
		{
			return false;
		}
		else
		{
			playerCash -= Costs[type];
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