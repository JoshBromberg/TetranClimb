using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasheSpawnController : EnemyController
{
    private Vector2 aggroRange = new Vector2(6f, 0.5f);
    private Transform player;
    private int spawnTimer = 0, spawnTimerReset = 10, spawnLimit = 8;
    [SerializeField]
    private GameObject rashe;
    // Start is called before the first frame update
    protected override void SpecificStart()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    protected override void SpecificUpdate()
    {
        if (moving == false)
        {
            moving = Mathf.Abs(transform.position.x - player.position.x) <= aggroRange.x && Mathf.Abs(transform.position.y - player.position.y) <= aggroRange.y;
        }
    }

    protected override void Move() //Spawns Rashe
    {
        if (spawnTimer <= 0 && spawnLimit > 0)
        {
            Instantiate(rashe, transform.position, transform.rotation);
            spawnTimer = spawnTimerReset;
            --spawnLimit;
        }
        else if (spawnTimer > 0)
        {
            --spawnTimer;
        }
    }
}
