using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerMidpoint : MonoBehaviour {
	public Transform player1;
	public Transform player2;

	void Update() {
		transform.position = (player1.position + player2.position) * 0.5f;
	}
}