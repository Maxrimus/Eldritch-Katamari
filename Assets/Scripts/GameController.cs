using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject enemyPrefab;
    float timer;

	// Use this for initialization
	void Start () {
        timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= 10)
        {
            timer -= 10;
            GameObject eP = Instantiate(enemyPrefab);
            int num = Random.Range(0, 1);
            float z;
            if (num == 0)
            {
                z = 0.1f;
            }
            else if (num == 1)
            {
                z = -0.1f;
            }
            else
            {
                z = 0.1f;
            }
            eP.GetComponent<Enemy>()._Move = new Vector3(0, 0, z);
            Vector3 pos = GetComponent<Transform>().position;
            eP.transform.position = pos;
        }
	}
}
