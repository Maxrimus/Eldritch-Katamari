﻿using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// If the R key is pressed
		if (Input.GetKeyDown(KeyCode.R))
		{
			// Check to make sure you're not in the starting screen
			if(Application.loadedLevelName != "MainMenu"){
				// Then reload level
				Application.LoadLevel(1);
			}
			
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			// Quit game
			Application.Quit();
		}
	}
}
