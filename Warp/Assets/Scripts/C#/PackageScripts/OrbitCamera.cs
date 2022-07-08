using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {
	public Transform target;
	public float edgeBorder = 0.1f;
	public float horizontalSpeed = 360.0f;
	public float verticalSpeed = 120.0f;
	public float minVertical = 20.0f;
	public float maxVertical = 85.0f;

	private float x = 0.0f;
	private float y = 0.0f;
	private float distance = 0.0f;

	void Start() {
		x = transform.eulerAngles.y;
		y = transform.eulerAngles.x;
		distance = (transform.position - target.position).magnitude;
	}

	void LateUpdate() {
		float dt = Time.deltaTime;
		x -= Input.GetAxis("Orbit X") * horizontalSpeed * dt;
		//y += Input.GetAxis("Vertical") * verticalSpeed * dt;

		y = ClampAngle(y, minVertical, maxVertical);

		Quaternion rotation = Quaternion.Euler(y, x, 0);
		Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

		transform.rotation = rotation;
		transform.position = position;
	}

	static float ClampAngle(float angle, float min, float max) {
		if(angle < -360)
			angle += 360;
		if(angle > 360)
			angle -= 360;
		return Mathf.Clamp(angle, min, max);
	}
}