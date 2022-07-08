using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDoor : MonoBehaviour {
	private float rayCastLength = 0.5f;
	private RaycastHit hit;
	public AudioClip gateClip;
	private bool gateMove = false;
	private GameObject gate;

	void Start() {
		gate = GameObject.FindWithTag("Gate");
	}

	void Update() {
		Vector3 forward = transform.TransformDirection(Vector3.forward) * rayCastLength;
		Debug.DrawRay(transform.position, forward, Color.white);

		// Check player collision using raycast
		if(Physics.Raycast(transform.position, transform.forward, out hit, rayCastLength)) {
			// Check if gameObject is gate
			if(hit.collider.gameObject.tag == "Gate" && gateMove == false) {
				gateMove = true;

				if(GetComponent<AudioSource>()) {
					GetComponent<AudioSource>().clip = gateClip;
					GetComponent<AudioSource>().Play();
				}

				print("Open Gate");
				// Open gate
				hit.collider.gameObject.GetComponent<Animation>().Play("GateOpen");
			}

			if(hit.collider.gameObject.tag == "Sensor" && gateMove == true) {
				gateMove = false;

				if(GetComponent<AudioSource>()) {
					GetComponent<AudioSource>().clip = gateClip;
					GetComponent<AudioSource>().Play();
				}

				print("Close Gate");
				// Close gate
				gate.GetComponent<Animation>().Play("GateClose");
			}
		}
	}
}