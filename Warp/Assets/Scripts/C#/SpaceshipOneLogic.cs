using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipOneLogic : MonoBehaviour {
	private float translateSpeed = 2.0f;
	private float rotateSpeed = 2.0f;
	private CharacterController spaceshipController;
	private Vector3 moveDirection = Vector3.zero;
	private float gravity = 0.4f;
	private Vector3 angle;

	void Start() {
		spaceshipController = GetComponent<CharacterController>();
		GetComponent<Animation>().wrapMode = WrapMode.Loop;
	}

	void Update() {
		if(spaceshipController.isGrounded) {
			moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical (Player 1)"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= translateSpeed;
		}

		angle = transform.eulerAngles;
		angle.y += Input.GetAxis("Horizontal (Player 1)") * rotateSpeed;
		transform.eulerAngles = angle;
		moveDirection.y -= gravity * Time.deltaTime;
		spaceshipController.Move(moveDirection * Time.deltaTime);
	}
}