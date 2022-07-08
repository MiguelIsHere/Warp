using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]

public class UnlimitedRangeSpawn : MonoBehaviour {
	public GameObject enemyPrefab;
	private GameObject currentEnemy;
	private float respawnTimer;
	public float delayTime = 2.0f;
	public float spawnRange = 50.0f;
	private Transform target;
	private Vector3 distance;
	
	void Start() {
		respawnTimer = 0.0f;
		target = GameObject.FindWithTag("Player").transform;
	}

	void Update() {
		distance = transform.position - target.position;
		respawnTimer += Time.deltaTime;

		if(distance.magnitude < spawnRange) { // Check if player is within spawn range
			if(respawnTimer > delayTime) {
				currentEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
				respawnTimer = 0.0f;
			}
		}
	}

	void OnGUI() {
		GUI.Box(new Rect(20, 20, 50, 20), distance.magnitude.ToString("f2"));
	}
}