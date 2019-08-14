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
	[SerializeField] AudioClip enemyDamageSFX;
	[SerializeField] AudioClip EnemyDeathSFX;

	AudioSource myAudioSource;

	TextMesh textMesh;

	void Start()
	{
		textMesh = GetComponent<TextMesh>();
		myAudioSource = GetComponent<AudioSource>();
		AddCollider();
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

	private void AddCollider()
	{
		Collider colliderOnEnemy = gameObject.AddComponent<CapsuleCollider>();
		colliderOnEnemy.isTrigger = false;
	}

	private void ProcessHit()
	{
		hits = hits - 1;
		myAudioSource.PlayOneShot(enemyDamageSFX);
		hitParticlePrefab.Play();
	}

	private void KillEnemy()
	{
		var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
		vfx.Play();
		float destroyDelay = vfx.main.duration;
		destroyDelay = 2f;
		AudioSource.PlayClipAtPoint(EnemyDeathSFX, Camera.main.transform.position);

		Destroy(vfx.gameObject, destroyDelay);
		Destroy(gameObject);
	}

	private void UpdateDamageLeft()
	{
		textMesh = GetComponentInChildren<TextMesh>();
		string updateText = hits.ToString();
		textMesh.text = updateText;
	}
}
