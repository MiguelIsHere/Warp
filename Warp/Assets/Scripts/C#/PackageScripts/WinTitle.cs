using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class WinTitle : MonoBehaviour {
	public GUIStyle customStyle;

	void OnGUI() {
		GUI.Label(new Rect(130, 332, 500, 100), "YOU WIN", customStyle);
	}
}