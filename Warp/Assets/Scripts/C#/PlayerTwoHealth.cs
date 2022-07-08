using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class PlayerTwoHealth : MonoBehaviour {
	public Texture2D[] healthBar;
	private int i;
	public int health;
	public GUIStyle customStyle;

	void Start() {
		i = 8;
		health = 800;
	}

	void Update() {
		if(health > 700) {
			i = 8;
			return;
		} else if (health > 600) {
			i = 7;
			return;
		} else if (health > 500) {
			i = 6;
			return;
		} else if (health > 400) {
			i = 5;
			return;
		} else if (health > 300) {
			i = 4;
			return;
		} else if (health > 200) {
			i = 3;
			return;
		} else if (health > 100) {
			i = 2;
			return;
		} else if (health > 0) {
			i = 1;
			return;
		} else if (health <= 0) {
			i = 0;
			health = 0;
			//Application.LoadLevel("Lose");
		}
	}

	void OnGUI() {
		GUI.Label(new Rect(680, 10, 128, 64), healthBar[i]);
		GUI.Label(new Rect(695, 36, 80, 20), health.ToString(), customStyle);
	}
}