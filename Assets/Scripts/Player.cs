using UnityEngine;
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

	// Use this for initialization
	void Start () {
        level = 1;
        radius = 1;
        direction = 1;
        goal = 2;
        timer = 0;
        falling = true;
        jumping = false;
        playing = true;
		grounded = true;
		rot = new Quaternion (0, 0, 0, 1);
        score.text = "Current Size: " + radius + "\n Goal Size: 2";
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            falling = true;
			grounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            falling = false;
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
                radius += rNew;
                GameObject.Destroy(collision.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if(radius >= 2 && level == 1)
        {
            level = 2;
            spawner.transform.position = new Vector3(0.0f, 7.41f, 30.06f);
            cam.transform.position = new Vector3(9f, 1f, 30.06f);
            transform.position = new Vector3(0.29f,0.98f,30.06f);
            goal = 5;
        }
        if(radius >= 5)
        {
            playing = false;
            youWin.enabled = true;
            score.text = "Current Size: " + radius + "\n Goal Size: " + goal;
        }
        if(playing)
        {
            GetComponent<Transform>().localRotation = new Quaternion(0, 0, 0, 1);
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
                if (!jumping)
                {
                    jumping = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && level >= 2)
            {
                GameObject bC = Instantiate(bulletP);
                Vector3 pos = transform.position;
                bC.transform.position = pos;
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
            if (jumping)
            {
                timer += Time.deltaTime;
                if (timer >= 0.5f)
                {
                    jumping = false;
					falling = true;
                    timer = 0;
                }
                move.y = 0.1f;
            }
			transform.localRotation = rot;
            GetComponent<CharacterController>().Move(move);
            score.text = "Current Size: " + radius + "\n Goal Size: " + goal;
        }
    }
}
