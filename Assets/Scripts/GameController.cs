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
            Vector3 pos = GetComponent<Transform>().position;
            eP.transform.position = pos;
        }
	}
}
