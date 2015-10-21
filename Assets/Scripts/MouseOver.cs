using UnityEngine;
using System.Collections;

public class MouseOver : MonoBehaviour {
	

	// On start the text remains red
	void Start () {
		// DO NOTHING
	}

	// On mouse enter the text turns to red
	void OnMouseEnter () {
		GetComponent<Renderer>().material.color = Color.red;
	}

	// On mouse exit the text turns to black
	void OnMouseExit () {
		GetComponent<Renderer>().material.color = Color.black;
	}


}
