using UnityEngine;
using System.Collections;

public class MouseOver : MonoBehaviour {
	

	// On start the text remains red
	void Start () {
		GetComponent<TextMesh>().color = Color.black;
	}

	// On mouse enter the text turns to red
	void OnMouseEnter () {
		GetComponent<TextMesh>().color = Color.red;
	}

	// On mouse exit the text turns to black
	void OnMouseExit () {
		GetComponent<TextMesh>().color = Color.black;
	}


}
