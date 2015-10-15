﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private float radius;
    public float Radius
    {
        get { return radius; }
    }
    float timer;
    Vector3 move;

    bool falling;
    bool jumping;

	// Use this for initialization
	void Start () {
        radius = 1;
        timer = 0;
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
            jumping = false;
        }
        if(collision.gameObject.tag == "Enemy")
        {
            if(collision.gameObject.GetComponent<Enemy>().radius <= radius)
            {
                float v1 = (4.0f / 3.0f) * Mathf.PI * Mathf.Pow(radius,3);
                float v2 = (4.0f / 3.0f) * Mathf.PI * Mathf.Pow(collision.gameObject.GetComponent<Enemy>().radius,3);
                float volTot = v1 + v2;
                float rb = Mathf.Pow((volTot * 3.0f) / (4.0f * Mathf.PI), (1.0f / 3.0f));
                float rNew = rb - radius;
                radius = rNew;
                GameObject.Destroy(collision.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        GetComponent<Transform>().localRotation = new Quaternion(0, 0, 0, 1);
        move = new Vector3(0,0,0);
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            move = new Vector3(-0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move = new Vector3(0.1f, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!jumping)
            {
                jumping = true;
            }
        }
        if (falling && !jumping)
        {
            move.y = -0.1f;
            timer = 0;
        }
        if(jumping)
        {
            timer += Time.deltaTime;
            if(timer >= 0.5f)
            {
                jumping = false;
                timer = 0;
            }
            move.y = 0.1f;
        }
        GetComponent<CharacterController>().Move(move);
	}
}
