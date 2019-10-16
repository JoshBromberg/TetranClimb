using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasheSpawnController : EnemyController
{
    private Vector2 aggroRange = new Vector2(7.5f, 0.5f);
    private Transform player;
    private int spawnTimer = 0, spawnTimerReset = 10, spawnLimit = 8;
    private SpriteRenderer sr;
    [SerializeField]
    private GameObject rashe;
    [SerializeField]
    private Transform rasheSpawnPoint;
    [SerializeField]
    private Sprite active, inactive;
    // Start is called before the first frame update
    protected override void SpecificStart()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected override void SpecificUpdate()
    {
        if (moving == false)
        {
            moving = Mathf.Abs(transform.position.x - player.position.x) <= aggroRange.x && Mathf.Abs(transform.position.y - player.position.y) <= aggroRange.y;
            if (moving)
            {
                sr.sprite = active;
            }
        }
    }

    protected override void Move() //Spawns Rashe
    {
        if (spawnTimer <= 0 && spawnLimit > 0)
        {
            Instantiate(rashe, rasheSpawnPoint.position, rasheSpawnPoint.rotation);
            spawnTimer = spawnTimerReset;
            --spawnLimit;
            if (spawnLimit == 0)
            {
                sr.sprite = inactive;
            }
        }
        else if (spawnTimer > 0)
        {
            --spawnTimer;
        }
    }
}
