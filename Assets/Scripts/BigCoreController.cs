using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCoreController : MonoBehaviour
{
    //Public Variables
    public float speedFactor;
    public Vector2 speed;
    public int bossPauseTimer, bossPauseTimerReset; //25
    public Sprite blueCore;
    public GameObject bigCoreLaser;
    public Transform[] bigCoreLaserSpawns = new Transform[4];
    public bool redCore = true;
    public GameObject core;
    public AudioClip hit;

    //Private Variables
    private float[] fireLocations = { 3.5f, 0, -3.5f };
    private Collider2D[] colliders;


    // Start is called before the first frame update
    void Start()
    {
        colliders = GetComponentsInChildren<Collider2D>();
        speed = new Vector2(speedFactor, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D rBody = GetComponent<Rigidbody2D>();

        //Move Boss
        if (bossPauseTimer == 0) {
            rBody.position += speed;
            if (rBody.position.x >= 3 - 0.001f && rBody.position.x <= 3 + 0.001f && rBody.position.x != 3)
            {
                speed = new Vector2(0, speedFactor);
                rBody.position = new Vector2(3, rBody.position.y);
            }
            for (int z = 0; z < fireLocations.Length; ++z)
            {
                if (rBody.position.y >= fireLocations[z] - 0.001f && rBody.position.y <= fireLocations[z] + 0.001f && rBody.position.x == 3)
                {
                    fire();
                    if (z != 1)
                    {
                        speed = -speed;

                        //Red Core Timer
                        if (z == 0 && redCore)
                        {
                            
                            core.GetComponent<SpriteRenderer>().sprite = blueCore;
                            redCore = false;

                        }
                    }
                }
            }
        }
        else
        {
            --bossPauseTimer;
        }

        //Boss Collision Commands
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            GameObject temp = Instantiate(bigCoreLaser, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            Destroy(temp.GetComponent<SpriteRenderer>());
            Destroy(temp, 0.04f);
        }
    }
    private void fire()
    {
        for (int z = 0; z < bigCoreLaserSpawns.Length; ++z)
        {
            Instantiate(bigCoreLaser, bigCoreLaserSpawns[z].position, bigCoreLaserSpawns[z].rotation);
        }
        bossPauseTimer = bossPauseTimerReset;
    }
}
