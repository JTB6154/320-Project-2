using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType { 
	Archer,
	Wizard,
	Theurgist
}

public class GameStats : Singleton<GameStats>
{
	Dictionary<TowerType, int> Costs = new Dictionary<TowerType, int>();
	int playerCash = 19;

	public void initialize()
	{

	}

	public int getCost(TowerType type)
	{
		return Costs[type];
	}

	/// <summary>
	/// Manages the player gold for purchasing an item of that type
	/// </summary>
	/// <param name="type"></param>
	public bool purchaseItemOfType(TowerType type)
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
	public void addGold(int amount)
	{
		playerCash += amount;
	}

}
