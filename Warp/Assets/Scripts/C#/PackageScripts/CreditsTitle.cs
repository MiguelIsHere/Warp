using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode()]

public class CreditsTitle : MonoBehaviour {
	public GUIStyle customStyle1;
	public GUIStyle customStyle2;

	void OnGUI() {
		GUI.Label(new Rect((Screen.width - 600) * 0.5f, 362, 600, 100), "CREDITS", customStyle1);
		GUI.Label(new Rect((Screen.width - 1000) * 0.5f, 402, 1000, 100), "STUDENT 1 NAME | STUDENT 1 ID | CLASS", customStyle2);
		GUI.Label(new Rect((Screen.width - 1000) * 0.5f, 432, 1000, 100), "STUDENT 2 NAME | STUDENT 2 ID | CLASS", customStyle2);
	}
}