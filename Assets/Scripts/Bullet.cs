using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    Vector3 move;

	// Use this for initialization
	void Start () {
        move = new Vector3(0.0f, 0.0f, 10f);
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
