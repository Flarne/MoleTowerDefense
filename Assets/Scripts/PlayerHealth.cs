using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField] int health = 20;
	[SerializeField] int decreaseHealth = 1;
	[SerializeField] Text healthText;

	private void Start()
	{
		healthText.text = health.ToString();
	}

	private void OnTriggerEnter(Collider other)
	{
		health = health - decreaseHealth;
		healthText.text = health.ToString();
	}
}
