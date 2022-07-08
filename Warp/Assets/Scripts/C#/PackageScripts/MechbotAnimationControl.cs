using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechbotAnimationControl : MonoBehaviour {
	// Animation control for Mechbot
	void Start() {
		GetComponent<Animation>()["walk_forward"].wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["walk_forward"].speed = 1;
	}
}