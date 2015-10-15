using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    Vector3 move;

	// Use this for initialization
	void Start () {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Direction == 1)
        {
            move = new Vector3(0.0f, 0.0f, -10f);
        }
        else if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Direction == 2)
        {
            move = new Vector3(0.0f, 0.0f, 10f);
        }
        else
        {
            move = new Vector3(0.0f, 0.0f, 10f);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy2")
        {
            collision.gameObject.GetComponent<Enemy>().breakUp();
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Platform")
        {
            Destroy(this.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().velocity = move;
	}
}
