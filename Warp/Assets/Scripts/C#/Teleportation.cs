using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour {
	public Transform eastPortal;
	public Transform westPortal;

	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(hit.gameObject.name == "West Portal")
			GetComponent<Collider>().transform.position = eastPortal.transform.position;
		if(hit.gameObject.name == "East Portal")
			GetComponent<Collider>().transform.position = westPortal.transform.position;
	}
}