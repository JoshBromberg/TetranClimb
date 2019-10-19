using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossModeController : MonoBehaviour
{
    private bool active = false;
    private AudioSource s;
    private float fadeOutTimer = 1, newFadeTimer = 1;
    [SerializeField]
    private AudioClip aircraftCarrierStart, aircraftCarrierLoop, poisonOfSnake;
    private int zubRushTimer = 750, currentZub = 0;
    [SerializeField]
    private GameObject smallPlatform, zub, tetran;
    private Vector2[] zubSpawn = new Vector2[15];
    [SerializeField]
    private Transform tetSpawn;
    // Start is called before the first frame update
    void Start()
    {
        s = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        int iteration = 0;
        for (int y = 0; y < 3; ++y)
        {
            for (int z = 0; z < 5; ++z)
            {
                zubSpawn[iteration] = new Vector2(-7+(z*3.5f), 27-y);
                ++iteration;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (fadeOutTimer > 0)
            {
                fadeOutTimer -= 0.01f;
                s.volume = fadeOutTimer > 0 ? fadeOutTimer : 1;
                if (fadeOutTimer <= 0)
                {
                    s.clip = aircraftCarrierStart;
                    s.loop = false;
                    s.Play();
                }
            }
            else if (zubRushTimer > 0)
            {
                if (s.isPlaying == false)
                {
                    s.clip = aircraftCarrierLoop;
                    s.loop = true;
                    s.Play();
                }
                if (zubRushTimer % 25 == 0)
                {
                    Instantiate(zub, zubSpawn[currentZub], Quaternion.identity);
                    currentZub = currentZub + 1 >= zubSpawn.Length ? 0 : ++currentZub;
                }
                --zubRushTimer;
                if (zubRushTimer <= 100)
                {
                    s.volume = zubRushTimer > 0 ? newFadeTimer: 1;
                    newFadeTimer -= 0.01f;
                    if (zubRushTimer == 0)
                    {
                        s.clip = poisonOfSnake;
                        s.Play();
                        Instantiate(tetran, transform.position + new Vector3(0, 8.5f), tetSpawn.rotation);
                    }
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (active == false && col.gameObject.tag == "Body")
        {
            GameObject b = GameObject.FindGameObjectWithTag("Boundry");
            b.transform.position = new Vector2(0, 35);
            b.transform.localScale = new Vector3(30, 30, 1);
            b.GetComponent<BoundryController>().playerSpawn = new Vector2(0, 23.5f);
            Instantiate(smallPlatform, new Vector2(-0.94f, 21.5f), Quaternion.identity);
            Instantiate(smallPlatform, new Vector2(0.94f, 21.5f), Quaternion.identity);
            string[] st = { "Rock", "Enemy" };
            foreach (string s in st)
            {
                foreach (GameObject g in GameObject.FindGameObjectsWithTag(s))
                {
                    if (g.transform.position.y < 21.5f)
                    {
                        Destroy(g);
                    }
                }
            }
            active = true;
        }
    }
}
