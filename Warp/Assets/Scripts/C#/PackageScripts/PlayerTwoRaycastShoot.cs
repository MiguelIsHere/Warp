using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoRaycastShoot : MonoBehaviour {
	private float range;
	//private float force = 10.0f;
	private RaycastHit hit;
	public Rigidbody laserTrail;
	private int speed;
	public GameObject explosionPrefab;
	public AudioClip fireClip;
	public AudioClip explodeClip;
	private GameObject controller;
	private PlayerOneHealth script;

void Start() {
		speed = 10;
		range = 2.6f;
		controller = GameObject.Find("Game Control Center");
		script = controller.transform.gameObject.GetComponent<PlayerOneHealth>();
	}

	void Update() {
		if(Input.GetButtonDown("Fire1 (Player 2)")) {
			Fire();
		}
	}

	void Fire() {
		Rigidbody laserTrailClone = Instantiate(laserTrail, transform.position, transform.rotation);
		Vector3 leftDirection = transform.TransformDirection(new Vector3(0.07f, 0, 1));
		Vector3 rightDirection = transform.TransformDirection(new Vector3(-0.07f, 0, 1));
		//Quaternion hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

		// Check if raycast hit anything
		if(this.name == "Left Gun") {
			// Laser trail
			laserTrailClone.velocity = transform.TransformDirection(new Vector3(0.07f, 0, 1) * speed);
			Destroy(laserTrailClone.gameObject, 0.26f);

			Debug.DrawRay(transform.position, leftDirection * range, Color.white);
			AudioSource.PlayClipAtPoint(fireClip, transform.position);
			if(Physics.Raycast(transform.position, leftDirection, out hit, range)) {
				if (hit.collider.gameObject.tag == "Enemy 1" || hit.collider.gameObject.tag == "Enemy 2" || hit.collider.gameObject.tag == "Enemy 3") {
					Destroy(hit.collider.gameObject);
					Instantiate(explosionPrefab, hit.transform.position, transform.rotation);
					AudioSource.PlayClipAtPoint(explodeClip, transform.position);
				}

				if(hit.collider.gameObject.tag == "Player") {
					if (script.health != 0)
						script.health -= 100;
					else
						script.health = 0;
				}
			}
		}

		if(this.name == "Right Gun") {
			// Laser trail
			laserTrailClone.velocity = transform.TransformDirection(new Vector3(-0.07f, 0, 1) * speed);
			Destroy(laserTrailClone.gameObject, 0.26f);

			Debug.DrawRay(transform.position, rightDirection * range, Color.white);
			if(Physics.Raycast(transform.position, rightDirection, out hit, range)) {
				if(hit.collider.gameObject.tag == "Enemy") {
					Destroy(hit.collider.gameObject);
				}
			}
		}
	}
}