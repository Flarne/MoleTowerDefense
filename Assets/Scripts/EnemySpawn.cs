using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
	[Range (0.1f, 120f)]
	[SerializeField] float secondsBetweenSpawns = 4f;
	[SerializeField] EnemyMovement enemyPrefab;
	[SerializeField] Transform enemyParentTransform;
	[SerializeField] AudioClip enemySpawnSFX;

    void Start()
    {
		StartCoroutine(SpawnEnemies());
    }

	IEnumerator SpawnEnemies()
	{
		while (true) // Forever
		{
			GetComponent<AudioSource>().PlayOneShot(enemySpawnSFX);
			var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
			newEnemy.transform.parent = enemyParentTransform;
			print("Spawning");

			yield return new WaitForSeconds(secondsBetweenSpawns);
		}
	}
}
