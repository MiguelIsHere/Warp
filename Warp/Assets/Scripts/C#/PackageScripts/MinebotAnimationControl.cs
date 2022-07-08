using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinebotAnimationControl : MonoBehaviour {
	// Animation control for Minebot
	void Start() {
		GetComponent<Animation>()["forward"].wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["forward"].speed = 1;
	}
}