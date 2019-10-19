using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DUCKERController : FanController
{
    private int attackCooldown = 0, attackCooldownReset = 100, moveCooldown = 10, moveCooldownReset = 10;
    private bool reverse = false;
    [SerializeField]
    private GameObject missile;
    [SerializeField]
    private Transform missileSpawn;
    private Transform player;
    private Vector2 fireRange = new Vector2(7.5f, 0.5f);
    private SpriteRenderer sr;
    [SerializeField]
    private Sprite move1, move2;
    private bool moveFirst = true;


    protected override void SpecificStart()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        base.SpecificStart();
    }
    protected override void SpecificUpdate()
    {
        if (attackCooldown > 0)
        {
            --attackCooldown;
        }
        --moveCooldown;
        if (moveCooldown <= 0)
        {
            sr.sprite = moveFirst ? move2 : move1;
            moveFirst = moveFirst ? false : true;
            moveCooldown = moveCooldownReset;
        }
        if (Mathf.Abs(transform.position.x - player.position.x) <= fireRange.x && Mathf.Abs(transform.position.y - player.position.y) <= fireRange.y)
        {
            if (player.position.x < transform.position.x && reverse == false)
            {
                transform.Rotate(0, 180, 0);
                reverse = true;
            }
            else if (player.position.x > transform.position.x && reverse)
            {
                transform.Rotate(0, -180, 0);
                reverse = false;
            }
            if (attackCooldown <= 0)
            {
                attackCooldown = attackCooldownReset;
                Instantiate(missile, missileSpawn.position, missileSpawn.rotation);
            }
        }
    }
}
