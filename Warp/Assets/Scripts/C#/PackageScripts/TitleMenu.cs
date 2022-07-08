using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]

public class TitleMenu : MonoBehaviour {
	public GUIStyle play;
	public GUIStyle instructions;
	public GUIStyle credits;
	public GUIStyle exit;

	void OnGUI() {
		if(GUI.Button(new Rect(900, 450, 182, 50), "PLAY", play))
			SceneManager.LoadScene("Game");
		if(GUI.Button(new Rect(900, 500, 182, 50), "INSTRUCTIONS", instructions))
			SceneManager.LoadScene("Instructions");
		if(GUI.Button(new Rect(900, 550, 182, 50), "CREDITS", credits))
			SceneManager.LoadScene("Credits");
		if(GUI.Button(new Rect(900, 600, 182, 50), "EXIT", exit))
			Application.Quit();
	}
}