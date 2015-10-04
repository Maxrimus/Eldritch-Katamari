using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    float timer;
    public GameObject enemyPrefab;
    public GameObject Player;
    //float radiusP;

    // Use this for initialization
    void Start () {
        timer = 0;
        //radiusP = Player.GetComponent<Controller>().Radius;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        //radiusP = Player.GetComponent<Controller>().Radius;
        if (timer >= 1)
        {
            timer -= 1;
            //float radius = Random.Range(radiusP - (radiusP / 100), radiusP + (radiusP / 100));
            float radius = Random.Range(0.1f, 2.0f);
            Vector3 position = new Vector3(Random.Range(-5.5f, 5.5f), radius/2.0f, Random.Range(-5.5f, 5.5f));
            GameObject obj = (GameObject)Instantiate(enemyPrefab, position, new Quaternion());
            obj.GetComponent<Enemy>().radius = radius;
            obj.transform.localScale = new Vector3(obj.GetComponent<Enemy>().radius, obj.GetComponent<Enemy>().radius, obj.GetComponent<Enemy>().radius);
        }
	}
}
