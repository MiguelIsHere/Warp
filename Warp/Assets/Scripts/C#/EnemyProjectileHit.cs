using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class EnemyProjectileHit : MonoBehaviour {
	public AudioClip explodeClip;
	public GameObject explodePrefab;

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Bullet 1" || collision.gameObject.tag == "Bullet 2") {
			Instantiate(explodePrefab, transform.position, transform.rotation);
			AudioSource.PlayClipAtPoint(explodeClip, transform.position);
			Destroy(gameObject);
			Destroy(collision.gameObject);
		}
	}
}