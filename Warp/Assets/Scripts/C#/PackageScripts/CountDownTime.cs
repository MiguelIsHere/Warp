using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode()]

public class CountDownTime : MonoBehaviour {
	private float startTime; // Time given to complete game
	private float timeRemaining;
	private string timeString;
	public GUIStyle customStyle;
	
	void Start() {
		startTime = 300;
	}

	void Update() {
		CountDown();
	}

	void CountDown() {
		timeRemaining = startTime - Time.timeSinceLevelLoad;
		ShowTime();

		if(timeRemaining < 0) {
			timeRemaining = 0;
			TimeIsUp();
		}
	}

	void ShowTime() {
		int minutes;
		int seconds;

		minutes = (int)timeRemaining / 60; // Derive minutes by dividing seconds by 60 seconds
		seconds = (int)timeRemaining % 60; // Derive remainder after dividing by 60 seconds
		timeString = "TIME LEFT > " + minutes.ToString() + ":" + seconds.ToString("d2");
	}

	void TimeIsUp() {
		SceneManager.LoadScene("Lose");
	}

	void OnGUI() {
		GUI.Label(new Rect((Screen.width - 300) * 0.5f, 662, 300, 20), timeString, customStyle);
	}
}