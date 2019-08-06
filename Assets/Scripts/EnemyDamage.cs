using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
	[SerializeField] GameObject hitEffect;
	[SerializeField] Transform parent;
	[SerializeField] int scorePerHit = 12;
	[SerializeField] int hits = 10;

	void Start()
	{
		AddBoxcollider();
	}

	private void OnParticleCollision(GameObject other)
	{
		ProcessHit();
		print("I'm hit!!!");
		if (hits <= 1)
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
		GameObject hitFX = Instantiate(hitEffect, transform.position, Quaternion.identity);
		hitFX.transform.parent = parent;
		Destroy(gameObject);
	}

	private void ProcessHit()
	{
		hits = hits - 1;
	}
}
