using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawning : MonoBehaviour {
	public GameObject spawnPrefab;

	void Start() {
		Instantiate(spawnPrefab, new Vector3(Random.Range(-8, 8), 0, 8), transform.rotation);
	}
}