using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
	[SerializeField] int towerLimit = 5;
	[SerializeField] Tower placeTower;
	[SerializeField] Transform towerParentTransform;

	Queue<Tower> towerQueue = new Queue<Tower>();

	public void AddTower(Waypoint baseWaypoint)
	{
		int numTowers = towerQueue.Count;
		
		if (numTowers < towerLimit)
		{
			InstantiateNewTower(baseWaypoint);
		}
		else
		{
			MoveExistingTower(baseWaypoint);
		}
	}

	private void InstantiateNewTower(Waypoint baseWaypoint)
	{
		var newTower = Instantiate(placeTower, baseWaypoint.transform.position, Quaternion.identity);
		newTower.transform.parent = towerParentTransform;

		// set the baseWaypoints
		newTower.baseWaypoint = baseWaypoint;
		baseWaypoint.isPlacable = false;

		towerQueue.Enqueue(newTower);
	}

	private void MoveExistingTower(Waypoint newBaseWaypoint)
	{
		// take bottom tower off queue
		var oldTower = towerQueue.Dequeue();

		// set the placable flags
		oldTower.baseWaypoint.isPlacable = true; // free up the block
		newBaseWaypoint.isPlacable = false;

		// set the baseWaypoints
		oldTower.baseWaypoint = newBaseWaypoint;

		oldTower.transform.position = newBaseWaypoint.transform.position;

		// put the old tower on top of the queue
		towerQueue.Enqueue(oldTower);
	}
}
