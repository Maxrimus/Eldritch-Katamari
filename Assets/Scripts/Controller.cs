using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
    float radius;
    public float xScale;
    public float zScale;
    private float height;
    private bool playing;
    private int speedMult;

    public float Radius
    {
        get { return radius; }
    }

	// Use this for initialization
	void Start () {
        radius = .5f;
        playing = true;
        speedMult = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if(radius >= 12)
        {
            playing = false;
        }
        if (playing)
        {
            if (radius >= 5)
            {
                speedMult = 2;
            }
            if (radius >= 10)
            {
                speedMult = 3;
            }
            Vector3 position;
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                position = transform.position;
                position.x -= .1f * speedMult;
                transform.position = position;
            }
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                position = transform.position;
                position.z += .1f * speedMult;
                transform.position = position;
            }
			if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                position = transform.position;
                position.z -= .1f * speedMult;
                transform.position = position;
            }
			if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                position = transform.position;
                position.x += .1f * speedMult;
                transform.position = position;
            }
            position = transform.position;
            position.y = (transform.localScale.y / 2) + height;
            transform.position = position;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(playing)
        {
            if ((collision.gameObject.tag != "Terrain" || collision.gameObject.tag != "Wall") && collision.gameObject.GetComponent<Enemy>().radius <= radius)
            {
                Absorb(collision.gameObject.GetComponent<Enemy>());
                GameObject.Destroy(collision.gameObject);
            }
            else
            {
                height += collision.gameObject.transform.localScale.y;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(playing)
        {
            if (collision.gameObject.tag != "Terrain")
            {
                height -= collision.gameObject.transform.localScale.y;
            }
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
        transform.localScale = new Vector3(xScale * radius, 1.0f, zScale * radius);
    }
}
