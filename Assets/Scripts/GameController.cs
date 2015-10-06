using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
    
    public GameObject Player;
    public Text winText;
    float radiusP;

    // Use this for initialization
    void Start () {
        winText.enabled = false;
        radiusP = Player.GetComponent<Controller>().Radius;
    }
	
	// Update is called once per frame
	void Update () {
        radiusP = Player.GetComponent<Controller>().Radius;
        if(radiusP >= 12)
        {
            winText.enabled = true;
        }
	}
}
