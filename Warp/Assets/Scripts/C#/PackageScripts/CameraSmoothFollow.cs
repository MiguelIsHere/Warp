/*
This camera smoothes out rotation around the y-axis and height
Horizontal distance to the target is always fixed
There are many different ways to smooth the rotation but doing it this way gives you a lot of control over how the camera behaves
For every of those smoothed values, calculate the wanted value and the current value
This is smoothened using the Lerp function
Finally the smoothed values are applied to the transform's position
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour {
	public Transform target; // Target following
	public float distance = 10.0f; // Distance in x-z plane to target
	public float height = 5.0f; // Height at which camera above target
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;

	void LateUpdate() {
		// Early out if no target
		if(!target)
			return;

		// Calculate current rotation angles
		float wantedRotationAngle = target.eulerAngles.y;
		float wantedHeight = target.position.y + height;

		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

		// Dampen rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

		// Dampen height
		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		// Convert angle into rotation
		Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

		// Set position of camera on x-z plane to distance behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;

		// Set height of camera
		Vector3 cameraPosition = transform.position;
		cameraPosition.y = currentHeight;
		transform.position = cameraPosition;

		// Look at target
		transform.LookAt(target);
	}
}