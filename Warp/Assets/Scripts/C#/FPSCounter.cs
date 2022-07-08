/* Attach this to a GUIText to make a frames/second indicator
It calculates frames/second over each updateInterval, so the display does not keep changing wildly
It is also fairly accurate at very low FPS counts (< 10)
We do this not by simply counting frames per interval, but by accumulatedFramesulating FPS for each frame
This way we end up with correct overall FPS even if the interval renders something like 5.5 frames
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {
	float updateInterval = 0.5f;
	private float accumulatedFrames = 0.0f; // Accumulated frames over the interval
	private int frames = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval
	public Text UIText;

	void Start() {
		if(!UIText) {
			Debug.LogWarning("FPS script needs a GUIText component!");
			enabled = false;
			return;
		}

		timeleft = updateInterval;
	}

	void Update() {
		timeleft -= Time.deltaTime;
		accumulatedFrames += Time.timeScale / Time.deltaTime;
		++frames;

		// Interval ended - update GUI text and start new interval
		if(timeleft <= 0.0) {
			// Display two fractional digits (f2 format)
			UIText.text = "" + (accumulatedFrames / frames).ToString("f2");
			timeleft = updateInterval;
			accumulatedFrames = 0.0f;
			frames = 0;
		}
	}
}