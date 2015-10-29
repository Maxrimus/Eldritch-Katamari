using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CutsceneController : MonoBehaviour
{
    public GameObject tutImg;
    public Material[] eat = new Material[11];
    public Material[] rage = new Material[27];
    public Material[] shooting = new Material[11];
    bool ate;
    bool raged;
    bool shot;

    // Use this for initialization
    void Start () {
        ate = false;
        raged = false;
        shot = false;
	}

    // Update is called once per frame
    void Update() {
        if (GameProperties.level == 1)
        {
            if (!ate)
            {
                StartCoroutine(PlayEat(0.2f));
                ate = true;
            }
        }
        if (GameProperties.level == 2)
        {
            if(!shot)
            {
                StartCoroutine(PlayShoot(0.2f));
                shot = true;
            }
        }
    }

    IEnumerator PlayEat(float s)
    {
        for (int i = 0; i < eat.Length; i++)
        {
            yield return new WaitForSeconds(s);
            tutImg.GetComponent<Renderer>().material = eat[i];
        }
        if (!raged)
        {
            StartCoroutine(PlayRage(0.2f));
            raged = true;
        }
    }

    IEnumerator PlayRage(float s)
    {
        for (int i = 0; i < rage.Length; i++)
        {
            yield return new WaitForSeconds(s);
            tutImg.GetComponent<Renderer>().material = rage[i];
        }
        Application.LoadLevel(1);
    }

    IEnumerator PlayShoot(float s)
    {
        for (int i = 0; i < shooting.Length; i++)
        {
            yield return new WaitForSeconds(s);
            tutImg.GetComponent<Renderer>().material = shooting[i];
        }
        Application.LoadLevel(1);
    }
}
