using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
	[SerializeField] Tower placeTower;
	[SerializeField] int towerLimit = 5;

	int numTowers = 0;

	public void AddTower(Waypoint baseWaypoint)
	{
		// var towers = FindObjectsOfType<Tower>();		// Another example of numTowers
		// int numberOfTowers = towers.Length;			// Another example of numTowers
		if (numTowers < towerLimit)
		{
			InstantiateNewTower(baseWaypoint);
		}
		else
		{
			MoveExistingTower();
		}
	}

	private void InstantiateNewTower(Waypoint baseWaypoint)
	{
		Instantiate(placeTower, baseWaypoint.transform.position, Quaternion.identity);
		baseWaypoint.isPlacable = false;
		numTowers++;                                    // Delete If Another example of numTowers
	}

	private static void MoveExistingTower()
	{
		print("To many towers");
	}
}
