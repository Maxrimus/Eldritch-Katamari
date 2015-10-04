using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
    public GameObject toFollow;
    private int angle;
    public int up;

	// Use this for initialization
	void Start () {
        angle = 90;
        up = 10;
        transform.Rotate(new Vector3(1, 0, 0), angle);
        transform.position = new Vector3(toFollow.transform.position.x, toFollow.transform.position.y + up, toFollow.transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
	}
}
