using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    Vector3 move;
    public Vector3 _Move
    {
        set { move = value; }
    }
    float damage;
    public float Damage
    {
        get { return damage; }
    }
    public Material enraged;
    bool falling;
    float timer;
    public float radius;
    public GameObject enemyS;
    float z;

	// Use this for initialization
	void Start () {
        z = 0;
        int num = (int)Random.Range(0, 2);
        switch(num)
        {
            case 0:
                z = 1.5f;
                break;
            case 1:
                z = -1.5f;
                break;
            default:
                z = 1.5f;
                break;
        }
        move = new Vector3(0, 0, 0);
        falling = true;
        damage = .03f;
        GetComponent<Rigidbody>().freezeRotation = true;
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            move.z *= -1;
            z *= -1;
        }
        if (collision.gameObject.tag == "Platform")
        {
            Vector3 hit = collision.contacts[0].normal;

            if (Vector3.Dot(hit, Vector3.forward) > 0 || Vector3.Dot(hit, Vector3.forward) < 0)
            {
                move.z = 0;
                falling = true;
            }
            else
            {
                move.z = z;
                falling = false;
            }
        }
        if(collision.gameObject.tag == "Pit")
        {
            z *= 1.15f;
            damage *= 1.15f;
            transform.position = GameObject.FindGameObjectWithTag("GameController").transform.position;
            GetComponent<Renderer>().material = enraged;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            falling = true;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        print("Hit");
        if (coll.gameObject.tag == "DeathBlocks1")
        {
            print("Blocks1");
            Vector3 newPos = new Vector3(0f, 7.41f, 0.0f);
            transform.position = newPos;
        }
        if (coll.gameObject.tag == "DeathBlocks2")
        {
            print("Blocks2");
            Vector3 newPos = new Vector3(0f, 7.41f, 30.06f);
            transform.position = newPos;
        }
    }

    public void breakUp()
    {
        Vector3 pos = transform.position;
        GameObject eP = Instantiate(enemyS);
        eP.transform.position = new Vector3(pos.x,pos.y,pos.z + 0.5f);
        eP = Instantiate(enemyS);
        eP.transform.position = new Vector3(pos.x, pos.y, pos.z - 0.5f);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
        if (falling)
        {
            move.y = -0.8f;
        }
        if(!falling)
        {
            move.y = 0.0f;
        }
        Vector3 posP = transform.position;
        posP.x = 0.3f;
        transform.position = posP;
        GetComponent<Rigidbody>().velocity = move;
	}
}
