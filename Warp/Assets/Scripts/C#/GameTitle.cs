using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class GameTitle : MonoBehaviour {
	public GUIStyle customStyle;

	void OnGUI() {
		GUI.Label(new Rect((Screen.width - 1000) * 0.5f, 80, 1000, 100), "GAME TITLE", customStyle);
	}
}