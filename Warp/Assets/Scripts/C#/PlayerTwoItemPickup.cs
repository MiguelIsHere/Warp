using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoItemPickup : MonoBehaviour {
	private GameObject controller;
	private int tokenValue;
	private TokenScore script;
	
	void Start() {
		controller = GameObject.Find("Game Controller");
		script = controller.transform.gameObject.GetComponent<TokenScore>();
		tokenValue = 10;
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject.tag == "Token") {
			// Add token to score
			script.player2Score += tokenValue;
			// Destroy token object
			Destroy(hit.gameObject);
		}
	}
}