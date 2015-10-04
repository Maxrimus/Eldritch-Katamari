using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
    float radius;

    public float Radius
    {
        get { return radius; }
    }

	// Use this for initialization
	void Start () {
        radius = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position;
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= -12)
        {
            position = transform.position;
            position.x-=.1f;
            transform.position = position;
        }
        if (Input.GetKey(KeyCode.UpArrow) && transform.position.z <= 5.5)
        {
            position = transform.position;
            position.z += .1f;
            transform.position = position;
        }
        if (Input.GetKey(KeyCode.DownArrow) && transform.position.z >= -5.5)
        {
            position = transform.position;
            position.z -= .1f;
            transform.position = position;
        }
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= 12)
        {
            position = transform.position;
            position.x += .1f;
            transform.position = position;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<Enemy>().radius <= radius)
        {
            Absorb(collision.gameObject.GetComponent<Enemy>());
            GameObject.Destroy(collision.gameObject);
        }
    }

    void Absorb(Enemy other)
    {
        float volumeThis = (4.0f/3.0f) * Mathf.PI * Mathf.Pow(radius, 3);
        float volumeOther = (4.0f / 3.0f) * Mathf.PI * Mathf.Pow(other.radius, 3);
        float volume = volumeOther + volumeThis;
        float rTotal = Mathf.Pow((volume * 3.0f) / (4.0f * Mathf.PI), (1.0f / 3.0f));
        float toAdd = rTotal - radius;
        radius += toAdd;
        transform.localScale = new Vector3(radius, radius, radius);
    }
}
