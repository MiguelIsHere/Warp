using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlayerOneProjectileHit : MonoBehaviour {
	public AudioClip explodeClip;
	public GameObject explodePrefab;
	private GameObject controller;
	private PlayerOneHealth script;

	void Start() {
		controller = GameObject.Find("Game Controller");
		script = controller.transform.gameObject.GetComponent<PlayerOneHealth>();
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Bullet 2") {
			Instantiate(explodePrefab, transform.position, transform.rotation);
			AudioSource.PlayClipAtPoint(explodeClip, transform.position);
			script.health -= 25;
			Destroy(collision.gameObject); // Destroy bullet
		}
	}
}