﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

    private float radius;
    public float Radius
    {
        get { return radius; }
    }
    float goal;

    private int level;
    public int Level
    {
        get { return level; }
    }
    private int direction;
    public int Direction
    {
        get { return direction; }
    }

    float timer;
    Vector3 move;

    bool falling;
    bool jumping;
    bool playing;
	bool grounded;

    Quaternion rot;

    public Text score;
    public Camera cam;
    public GameObject spawner;
    public GameObject bulletP;
    public Text youWin;

	// Create varaibles for the progressBar;
	float barInc = 0f;
	Vector2 pos = new Vector2(480,40);
	Vector2 size = new Vector2(260,40);
	Texture2D barBackground;
	Texture2D progressBarFull;

	// Use this for initialization
	void Start () {
        if(GameProperties.level == 2)
        {
            level = 2;
            goal = 2.0f;
            spawner.transform.position = new Vector3(0.3f, 7.41f, 30.06f);
            cam.transform.position = new Vector3(9f, 1f, 30.06f);
            transform.position = new Vector3(0.29f, 0.98f, 30.06f);
            radius = GameProperties.radius;
            direction = 1;
            timer = 0;
            falling = true;
            jumping = false;
            playing = true;
            grounded = true;
            rot = new Quaternion(0, 0, 0, 1);
            youWin.fontSize = 24;
            score.text = "Current Size: " + radius + "\n Goal Size: 2";
        }
        else
        {
            level = 1;
            GameProperties.level = 1;
            radius = 0.5f;
            GameProperties.radius = 0.5f;
            direction = 1;
            goal = 1.0f;
            timer = 0;
            falling = true;
            jumping = false;
            playing = true;
            grounded = true;
            rot = new Quaternion(0, 0, 0, 1);
            youWin.fontSize = 24;
            score.text = "Current Size: " + radius + "\n Goal Size: 2";

        }
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
            Vector3 hit = collision.contacts[0].normal;

            if (Vector3.Dot(hit, Vector3.up) > 0)
            {
                falling = false;
                grounded = true;
                jumping = false;
            }
            else if (Vector3.Dot(hit, Vector3.up) < 0)
            {
                falling = true;
                grounded = false;
                jumping = false;
            }
            else if (Vector3.Dot(hit, Vector3.forward) > 0 || Vector3.Dot(hit, Vector3.forward) < 0 && !grounded)
            {
                falling = false;
                grounded = false;
                jumping = true;
            }
        }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Enemy2")
        {
            if (collision.gameObject.GetComponent<Enemy>().radius <= radius)
            {
                float v1 = (4.0f / 3.0f) * Mathf.PI * Mathf.Pow(radius, 3);
                float v2 = (4.0f / 3.0f) * Mathf.PI * Mathf.Pow(collision.gameObject.GetComponent<Enemy>().radius, 3);
                float volTot = v1 + v2;
                float rb = Mathf.Pow((volTot * 3.0f) / (4.0f * Mathf.PI), (1.0f / 3.0f));
                float rNew = rb - radius;
                Vector3 posC = transform.position;
                posC.y += rNew;
                transform.position = posC;
                radius += rNew;
                GameObject.Destroy(collision.gameObject);
                GameProperties.radius = radius;
            }
            else if (collision.gameObject.GetComponent<Enemy>().radius >= radius)
            {
                radius -= collision.gameObject.GetComponent<Enemy>().Damage;
                GameProperties.radius = radius;
            }
        }
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            radius += .01f;
            GameProperties.radius = radius;
        }
        if(collision.gameObject.tag == "Pit")
        {
            playing = false;
            youWin.enabled = true;
            // youWin.text = "You Lose!";
            //  score.text = "Current Size: " + radius + "\n Goal Size: " + goal;
            GameProperties.level = 1;
            Application.LoadLevel(3);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        print("Hit");
        if(coll.gameObject.tag == "DeathBlocks1")
        {
            print("Blocks1");
            Vector3 newPos = new Vector3(0.0f, 0.668f, 0.668f);
            transform.position = newPos;
        }
        if (coll.gameObject.tag == "DeathBlocks2")
        {
            print("Blocks2");
            Vector3 newPos = new Vector3(0.0f, 0.98f, 30.06f);
            transform.position = newPos;
        }
    }

	void OnGUI(){
		
		// Draws a background to encase the progress bar
		GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y));
		GUI.Box (new Rect(0,0, size.x, size.y),barBackground);
		
		// Draws the progress bar
		GUI.BeginGroup (new Rect (0, 0, size.x * barInc, size.y));
		GUI.Box (new Rect (0,0, size.x, size.y),progressBarFull);
		GUI.EndGroup ();
		
		GUI.EndGroup ();
		
	}

    void keepPosition(Vector3 posP, float decrease)
    {
        Vector3 fixedPos = new Vector3(posP.x,posP.y + decrease,posP.z);
        transform.position = posP;
    }

    // Update is called once per frame
    void Update ()
    {
        if (radius >= goal && level == 1)
        {
            GameProperties.level = 2;
            GameProperties.radius = radius;
            Application.LoadLevel(4);
        }
        else if (radius >= 2.0f && level == 2)
        {
            playing = false;
            GameProperties.level = 1;
			Application.LoadLevel(2);
        }
        if(radius <= 0)
        {
            playing = false;
            youWin.enabled = true;
            GameProperties.level = 1;
            Application.LoadLevel(3);
        }
        GetComponent<Transform>().localRotation = new Quaternion(0, 0, 0, 1);
        if (playing)
        {
            move = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                move = new Vector3(0, 0, -0.1f);
                direction = 1;
				rot = new Quaternion(0,0,0,1);
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                move = new Vector3(0, 0, 0.1f);
				direction = 2;
				rot = new Quaternion(0,180,0,1);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (!jumping && grounded)
                {
                    jumping = true;
                    grounded = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && level >= 2)
            {
                GameObject bC = Instantiate(bulletP);
                Vector3 pos = transform.position;
                if(direction == 1)
                {
                    bC.transform.position = new Vector3(pos.x, pos.y, pos.z - (5 * radius) / 8);
                }
                if (direction == 2)
                {
                    bC.transform.position = new Vector3(pos.x, pos.y, pos.z + (5 * radius) / 8);
                }
                radius -= .01f;
                GameProperties.radius = radius;
                keepPosition(pos, .01f);
            }
			// If the R key is pressed
			if (Input.GetKeyDown(KeyCode.R))
			{
				// Then reload level
				Application.LoadLevel(Application.loadedLevel);
			}


			if (Input.GetKeyDown(KeyCode.Escape))
			{
				// Quit game
				Application.Quit();
			}
            if (falling && !jumping)
            {
                move.y = -0.1f;
                timer = 0;
            }
            if (jumping && !grounded)
            {
                timer += Time.deltaTime;
                if (timer >= 0.6f)
                {
                    jumping = false;
					falling = true;
                    timer = 0;
                }
                move.y = 0.1f;
            }
            Vector3 posC = transform.position;
            transform.localRotation = rot;
            float scale = 1.0f * (radius / goal);
            transform.localScale = new Vector3(0.3f, scale, scale);
            Vector3 posP = transform.position;
            posP.x = 0.3f;
            transform.position = posP;
            GetComponent<CharacterController>().Move(move);
			// Increase the bar as the radius increases
			// Multiply by .33 because the radius is a third of overall goal
			barInc = radius *0.33f;
            score.text = "Current Size: " + radius + "\n Goal Size: " + goal;
        }
    }
}
