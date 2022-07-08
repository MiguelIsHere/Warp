using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTitle : MonoBehaviour {
	public GUIStyle customStyle;

	void OnGUI() {
		GUI.Label(new Rect(600, 327, 500, 100), "YOU LOSE", customStyle);
	}
}