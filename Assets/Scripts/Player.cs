using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private float radius;
    public float Radius
    {
        get { return radius; }
    }

    bool falling;
    bool jumping;

	// Use this for initialization
	void Start () {
        radius = 1;
        falling = false;
        jumping = false;
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            falling = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            falling = false;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        GetComponent<Transform>().localRotation = new Quaternion(0, 0, 0, 1);
        Vector3 move = new Vector3(0,0,0);
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            move = new Vector3(0, 0, -0.1f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move = new Vector3(0, 0, 0.1f);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            jumping = true;
        }
        if (falling && !jumping)
        {
            move.y -= 0.1f;
        }
        if(jumping)
        {

        }
        GetComponent<CharacterController>().Move(move);
	}
}
