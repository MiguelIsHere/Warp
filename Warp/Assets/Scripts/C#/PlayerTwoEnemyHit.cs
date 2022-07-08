using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoEnemyHit : MonoBehaviour {
	private GameObject controller;
	private PlayerTwoHealth script;
	
	void Start() {
		controller = GameObject.Find("Game Controller");
		script = controller.transform.gameObject.GetComponent<PlayerTwoHealth>();
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(hit.gameObject.tag == "Enemy 1") {
			// Deduct health for damage dealt by enemy type
			script.health -= 100;
			// Destroy enemy controller upon collision
			Destroy(hit.gameObject);
		}

		if (hit.gameObject.tag == "Enemy 2") {
			script.health -= 200;
			Destroy(hit.gameObject);
		}

		if (hit.gameObject.tag == "Enemy 3") {
			script.health -= 300;
			Destroy(hit.gameObject);
		}
	}
}