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

	public int getCost(TowerType type)
	{
		return Costs[type];
	}


}
