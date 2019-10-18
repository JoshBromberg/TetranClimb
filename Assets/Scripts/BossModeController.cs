using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossModeController : MonoBehaviour
{
    private bool active = false;
    private AudioSource s;
    private float fadeOutTimer = 1;
    [SerializeField]
    private AudioClip aircraftCarrier, poisonOfSnake;
    private int zubRushTimer = 750, currentZub = 0;
    [SerializeField]
    private GameObject smallPlatform, zub;
    [SerializeField]
    private Transform[] zubSpawn = new Transform[15];
    // Start is called before the first frame update
    void Start()
    {
        s = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
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
                    s.clip = aircraftCarrier;
                }
            }
            else if (zubRushTimer > 0)
            {
                if (zubRushTimer % 10 == 0)
                {
                    Instantiate(zub, zubSpawn[currentZub].position, zubSpawn[currentZub].rotation);
                    currentZub = currentZub + 1 >= zubSpawn.Length ? 0 : ++currentZub;
                }
                --zubRushTimer;
                if (zubRushTimer <= 100)
                {
                    s.volume = zubRushTimer > 0 ? zubRushTimer / 100 : 1;
                    if (zubRushTimer == 0)
                    {
                        s.clip = poisonOfSnake;
                        //instantiate Tetran
                    }
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (active == false && col.gameObject.tag == "Player")
        {
            GameObject b = GameObject.FindGameObjectWithTag("Boundry");
            b.transform.position = new Vector2(0, 40);
            b.transform.localScale = new Vector3(30, 30, 1);
            b.GetComponent<BoundryController>().playerSpawn = new Vector2(0, 23.5f);
            Instantiate(smallPlatform, new Vector2(-0.94f, 21.5f), Quaternion.identity);
            Instantiate(smallPlatform, new Vector2(0.94f, 21.5f), Quaternion.identity);
            active = true;
        }
    }
}
