using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    Vector3 move;
    public Vector3 _Move
    {
        set { move = value; }
    }
    bool falling;
    bool jumping;
    float timer;
    public float radius;
    public GameObject enemyS;

	// Use this for initialization
	void Start () {
        float z = 0;
        int num = (int)Random.Range(0, 2);
        switch(num)
        {
            case 0:
                z = 0.5f;
                break;
            case 1:
                z = -0.5f;
                break;
            default:
                z = 0.5f;
                break;
        }
        move = new Vector3(0, 0, z);
        falling = true;
        jumping = false;
        GetComponent<Rigidbody>().freezeRotation = true;
	}

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            jumping = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            move.z *= -1;
        }
        if (collision.gameObject.tag == "Platform")
        {
            falling = false;
        }
    }

    public void breakUp()
    {
        Vector3 pos = transform.position;
        GameObject eP = Instantiate(enemyS);
        for(int i = 0; i < 16; i++)
        {
            eP.transform.position = pos;
            eP = Instantiate(enemyS);
        }
    }

    // Update is called once per frame
    void Update () {
        if (falling)
        {
            move.y = -0.8f;
        }
        if (jumping)
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                jumping = false;
                falling = true;
                timer = 0;
            }
            move.y = 0.4f;
        }
        if(!jumping && !falling)
        {
            move.y = 0.0f;
        }
        GetComponent<Rigidbody>().velocity = move;
	}
}
