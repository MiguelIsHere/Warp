using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour {
	private int orthographicSizeMin;
	private int orthographicSizeMax;

	void Start() {
		orthographicSizeMin = 2;
		orthographicSizeMax = 10;
	}

	void Update() {
		if(Input.GetAxis("Zoom") > 0 || Input.GetAxis("Mouse ScrollWheel") > 0) { // Zoom forward
			Camera.main.orthographicSize--;
		}

		if(Input.GetAxis("Zoom") < 0 || Input.GetAxis("Mouse ScrollWheel") < 0) { // Zoom backward
			Camera.main.orthographicSize++;
		}

		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, orthographicSizeMin, orthographicSizeMax);
	}
}