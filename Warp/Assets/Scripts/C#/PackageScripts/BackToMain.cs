using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode()]

public class BackToMain : MonoBehaviour {
	public GUIStyle backToMain;

	void OnGUI() {
		if(GUI.Button(new Rect(1000, 650, 200, 50), "BACK TO MAIN", backToMain))
			SceneManager.LoadScene("Title");
	}
}