using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class TokenScore : MonoBehaviour {
	public int player1Score;
	public int player2Score;
	public GUIStyle customStyle;

	void Start() {
		//Screen.showCursor = false;
		player1Score = 0;
		player2Score = 0;
	}

	void OnGUI() {
		GUI.Label(new Rect(5, 580, 200, 50), "SCORE > " + player1Score, customStyle);
		GUI.Label(new Rect(1040, 580, 200, 50), "SCORE > " + player2Score, customStyle);
	}
}