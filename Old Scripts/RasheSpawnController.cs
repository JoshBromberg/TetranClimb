using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasheSpawnController : MonoBehaviour
{
    //Public Variables
    public GameObject rashe;
    public int health = 5;
    public Sprite damaged;
    public AudioClip tookDamage;

    //Private Variables
    private int rasheSpawnTimer = 0, rasheSpawnTimerReset = 15, rasheSpawned = 0, rasheSpawnedMax = 8;
    private bool startSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startSpawning)
        {
            if (rasheSpawnTimer == 0 && rasheSpawned<rasheSpawnedMax)
            {
                Instantiate(rashe, transform.position, Quaternion.identity);
                rasheSpawnTimer = rasheSpawnTimerReset;
                ++rasheSpawned;
            }
            else if (rasheSpawnTimer > 0)
            {
                --rasheSpawnTimer;
            }
        }
        else if (gameObject.transform.position.x <= 5 && startSpawning == false)
        {
            startSpawning = true;
        }
        if (health == 1 && GetComponent<SpriteRenderer>().sprite != damaged)
        {
            GetComponent<SpriteRenderer>().sprite = damaged;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().SFXPlay(tookDamage);
        }
    }
}
