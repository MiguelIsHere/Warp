using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class InstructionsTitle : MonoBehaviour {
	public GUIStyle customStyle1;
	public GUIStyle customStyle2;

	void OnGUI() {
		GUI.Label(new Rect(570, 372, 600, 100), "INSTRUCTIONS", customStyle1);
		GUI.Label(new Rect(573, 417, 600, 100), "Button A > Shield", customStyle2);
		GUI.Label(new Rect(573, 452, 600, 100), "Button B > Fire", customStyle2);
	}
}