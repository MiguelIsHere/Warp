using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechbotPoseControl : MonoBehaviour {
	// Animation control for Mechbot
	void Start() {
		GetComponent<Animation>()["idle"].wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["idle"].speed = 0.5f;
	}
}