using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    float timer;
    float timerMax;
    public Player play;
    public Text instructions;
    public Text youWin;

    // Use this for initialization
    void Start () {
        timer = 0.0f;
        timerMax = 1.0f;
        instructions.text = "Eat Enemies to Progress!";
        youWin.enabled = false;
        Physics.IgnoreLayerCollision(8, 8);
        Physics.IgnoreLayerCollision(9, 9);
        Physics.IgnoreLayerCollision(8, 9);
        Physics.IgnoreLayerCollision(8, 10);
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        Vector3 pos;
        GameObject eP;
        if(timer >= timerMax)
        {
            timer -= timerMax;
            if(play.Level == 1)
            {
                eP = Instantiate(enemyPrefab1);
                pos = GetComponent<Transform>().position;
                eP.transform.position = pos;
            }
            else if(play.Level == 2)
            {
                timerMax = 2.0f;
                instructions.text = "Press Space!";
                int num = (int)Random.Range(0, 2);
                switch(num)
                {
                    case 0:
                        eP = Instantiate(enemyPrefab1);
                        pos = GetComponent<Transform>().position;
                        eP.transform.position = pos;
                        break;
                    case 1:
                        eP = Instantiate(enemyPrefab2);
                        pos = GetComponent<Transform>().position;
                        eP.transform.position = pos;
                        break;
                    default:
                        eP = Instantiate(enemyPrefab1);
                        pos = GetComponent<Transform>().position;
                        eP.transform.position = pos;
                        break;
                }
            }
        }
	}
}
