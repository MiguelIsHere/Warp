using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlimitedSpawn : MonoBehaviour {
	public GameObject enemyPrefab;
	private GameObject currentEnemy;
	private float respawnTimer;
	private float delayTime = 5.0f;

	void Start() {
		respawnTimer = 0.0f;
		currentEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
	}

	void Update() {
		respawnTimer += Time.deltaTime;
		if(respawnTimer > delayTime) {
			currentEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
			respawnTimer = 0.0f;
		}
	}
}