using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnePrefabShoot : MonoBehaviour {
	public Rigidbody projectile;
	private int speed;
	private int ammo;
	public AudioClip fireClip;

	void Start() {
		speed = 18;
		ammo = 100;
	}

	void Update() {
		if(Input.GetButtonDown("Fire1 (Player 1)")) {
			Rigidbody projectileClone = Instantiate(projectile, transform.position, transform.rotation);
			AudioSource.PlayClipAtPoint(fireClip, transform.position);
			projectileClone.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
			Destroy(projectileClone.gameObject, 0.15f);
			ammo--;
		}
	}
}