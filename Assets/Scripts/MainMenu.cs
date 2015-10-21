using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public bool isStartGame = true;

	// Use this for initialization
	void Start () {
	
	}

	// Performs an action when the mouse is up
	void OnMouseUp(){
		// if true then start
		if (isStartGame) {
			Application.LoadLevel(1);
		}
	}
}
