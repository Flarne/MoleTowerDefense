using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
	[Range (0.1f, 120f)]
	[SerializeField] float secondsBetweenSpawns = 2f;
	[SerializeField] EnemyMovement enemyPrefab;

    void Start()
    {
		StartCoroutine(SpawnEnemies());
    }

	IEnumerator SpawnEnemies()
	{
		while (true) // Forever
		{
			Instantiate(enemyPrefab, transform.position, Quaternion.identity);
			print("Spawning");

			yield return new WaitForSeconds(secondsBetweenSpawns);
		}
	}
}
