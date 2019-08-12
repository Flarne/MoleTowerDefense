using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
	[SerializeField] Transform parent;
	[SerializeField] int scorePerHit = 12;
	[SerializeField] int hits = 10;
	[SerializeField] ParticleSystem hitParticlePrefab;
	[SerializeField] ParticleSystem deathParticlePrefab;

	void Start()
	{
		AddBoxcollider();
	}

	private void OnParticleCollision(GameObject other)
	{
		ProcessHit();
		if (hits <= 0)
		{
			KillEnemy();
		}
		else
		{
			UpdateDamageLeft();
		}
	}

	private void UpdateDamageLeft()
	{
		TextMesh textMesh = GetComponentInChildren<TextMesh>();
		string updateText = hits.ToString();
		textMesh.text = updateText;
	}

	private void AddBoxcollider()
	{
		Collider colliderOnEnemy = gameObject.AddComponent<CapsuleCollider>();
		colliderOnEnemy.isTrigger = false;
	}

	private void KillEnemy()
	{
		var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
		vfx.Play();
		float destroyDelay = vfx.main.duration;
		destroyDelay = 2f;

		Destroy(vfx.gameObject, destroyDelay);
		Destroy(gameObject);
	}

	private void ProcessHit()
	{
		hits = hits - 1;
		hitParticlePrefab.Play();
	}
}
