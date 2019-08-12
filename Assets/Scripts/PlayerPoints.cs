using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPoints : MonoBehaviour
{
	[SerializeField] Text healthText;

	int spawnPoints;

	void Start()
    {
		healthText.text = spawnPoints.ToString();
    }

	private void OnTriggerExit(Collider other)
	{
		spawnPoints++;
		healthText.text = spawnPoints.ToString();
	}
}
