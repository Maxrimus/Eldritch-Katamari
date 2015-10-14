using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    Vector3 move;
    public Vector3 _Move
    {
        set { move = value; }
    }

	// Use this for initialization
	void Start () {
        move = new Vector3(0, 0, 0);
	}

    void OnCollisionEnter(Collision collision)
    {
        move.z *= -1;
    }

    // Update is called once per frame
    void Update () {
        GetComponent<CharacterController>().Move(move);
	}
}
