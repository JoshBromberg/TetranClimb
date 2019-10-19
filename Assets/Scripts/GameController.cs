using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int score, waitTimer;
    private int[] fansActive = { 4, 4, 4, 4 };
    [SerializeField]
    private GameObject powerCapsule, playerDeath, audioPlayer;
    [SerializeField]
    private AudioClip playerDeathSound, defeatMusic;
    private Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTimer > 0)
        {
            --waitTimer;
            if (waitTimer == 0)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = defeatMusic;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().loop = false;
            }
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
    public void AddScore(int i)
    {
        score += i;
        scoreText.text = "Score: " + score;
    }
    public void AddScore(int i, GameObject g, Transform t)
    {
        AddScore(i);
        Instantiate(g, t.position, t.rotation);
        if (i >= 50)
        {
            Instantiate(powerCapsule, t.position, t.rotation);
        }
        if (g.name.Contains("Fan"))
        {
            int fanArray = t.position.y > transform.position.y ? 0 : 2;
            fanArray = t.position.x > transform.position.x ? fanArray : fanArray + 1;
            --fansActive[fanArray];
            if (fansActive[fanArray] == 0)
            {
                Instantiate(powerCapsule, t.position, t.rotation);
            }
        }
    }
    public void Defeat(Transform t)
    {
        Instantiate(playerDeath, t.position, transform.rotation);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = playerDeathSound;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
        waitTimer = 150;
    }
}
