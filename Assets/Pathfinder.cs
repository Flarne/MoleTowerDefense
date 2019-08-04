using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
	[SerializeField] Waypoint startWaypoint, endWaypoint;

	Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
	Queue<Waypoint> queue = new Queue<Waypoint>();
	bool isRunning = true;
	Waypoint searchCenter;
	List<Waypoint> path = new List<Waypoint>();

	Vector2Int[] directions =
	{
		Vector2Int.up,
		Vector2Int.right,
		Vector2Int.down,
		Vector2Int.left
	};

	public List<Waypoint> GetPath()
	{
		LoadBlocks();
		StartAndEndColor();
		BreadthFirstSearch();
		CreatePath();

		// ExploreNeighbours();

		return path;
	}

	private void CreatePath()
	{
		path.Add(endWaypoint);

		Waypoint previousBlock = endWaypoint.exploredFrom;
		while (previousBlock != startWaypoint)
		{
			// Add intermediate waypoints
			previousBlock.SetTopColor(Color.black);
			path.Add(previousBlock);
			previousBlock = previousBlock.exploredFrom;
		}

		path.Add(startWaypoint);
		path.Reverse();
		// reverse list
	}

	private void BreadthFirstSearch()
	{
		queue.Enqueue(startWaypoint);

		while (queue.Count > 0 && isRunning)
		{
			searchCenter = queue.Dequeue();
			HaltIfEndFound();
			ExploreNeighbours();
			searchCenter.isExplored = true;
		}
	}

	private void HaltIfEndFound()
	{
		if (searchCenter == endWaypoint)
		{
			isRunning = false;
		}
	}

	private void ExploreNeighbours()
	{
		if (!isRunning) { return; }

		foreach (Vector2Int direction in directions)
		{
			Vector2Int neighboursCoords = searchCenter.GetGridPos() + direction;
			if (grid.ContainsKey(neighboursCoords))
			{
				QueueNewNeighbours(neighboursCoords);
			}
		}
	}

	private void QueueNewNeighbours(Vector2Int neighboursCoords)
	{
		Waypoint neighbour = grid[neighboursCoords];
		if (neighbour.isExplored || queue.Contains(neighbour))
		{
			// Do nothing
		}
		else
		{
			queue.Enqueue(neighbour);
			neighbour.exploredFrom = searchCenter;
		}
	}

	private void StartAndEndColor()
	{
		// todo consider moving out
		startWaypoint.SetTopColor(Color.green);
		endWaypoint.SetTopColor(Color.red);
	}

	private void LoadBlocks()
	{
		var waypoints = FindObjectsOfType<Waypoint>();
		foreach (Waypoint waypoint in waypoints)
		{
			var gridPos = waypoint.GetGridPos();
			if (grid.ContainsKey(gridPos))
			{
				Debug.LogWarning("Skipping overlapping Block " + waypoint);
			}
			else
			{
				grid.Add(gridPos, waypoint);
				//waypoint.SetTopColor(Color.black);
			}
		}
	}
}
