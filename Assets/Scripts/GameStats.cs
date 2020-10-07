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

	public void Initialize()
	{

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
	/// Manages the player gold for purchasing an item of that type
	/// </summary>
	/// <param name="type"></param>
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