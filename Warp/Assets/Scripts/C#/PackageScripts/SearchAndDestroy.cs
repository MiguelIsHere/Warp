using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchAndDestroy : MonoBehaviour {
	private NavMeshAgent agent;
	private Vector3 target;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
	}

	void Update() {
		target = GameObject.FindWithTag("Player").transform.position;
		agent.destination = target;
	}

	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.tag == "Player") {
			Destroy(this.gameObject);
		}
	}
}