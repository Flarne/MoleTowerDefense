using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] float enemySpeed = 2f;
	[SerializeField] ParticleSystem enemySelfDestruct;

    void Start()
	{
		Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
		var path = pathfinder.GetPath();
		StartCoroutine(FollowPath(path));
    }

	IEnumerator FollowPath(List<Waypoint> path)
	{
		print("Starting Patrol:");
		foreach (Waypoint waypoint in path)
		{
			transform.position = waypoint.transform.position;
			yield return new WaitForSeconds(enemySpeed);
		}
		SelfDestruct();
	}

	private void SelfDestruct()
	{
		var vfx = Instantiate(enemySelfDestruct, transform.position, Quaternion.identity);
		vfx.Play();
		float destroyDelay = vfx.main.duration;
		destroyDelay = 1f;

		Destroy(vfx.gameObject, destroyDelay);
		Destroy(gameObject);
	}
}
