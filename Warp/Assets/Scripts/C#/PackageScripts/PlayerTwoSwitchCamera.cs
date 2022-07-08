using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoSwitchCamera : MonoBehaviour {
	private int i;
	public Camera camera1;
	public Camera camera2;

	void Start() {
		camera1.enabled = true;
		camera2.enabled = false;
	}

	void Update() {
		if(Input.GetKeyDown("9")) {
			camera1.enabled = true;
			camera2.enabled = false;
		}
		if(Input.GetKeyDown("0")) {
			camera1.enabled = false;
			camera2.enabled = true;
		}

		if(Input.GetButtonDown("Switch (Player 2)")) {
			i += 1;
			if (i > 2)
				i = 1;
		}

		if(i == 1) {
			camera1.enabled = true;
			camera2.enabled = false;
		} else if (i == 2) {
			camera1.enabled = false;
			camera2.enabled = true;
		}
	}
}