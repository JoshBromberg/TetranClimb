using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarrunController : EnemyController
{
    private Vector2 aggroRange = new Vector2(7.5f, 1);
    private float margainOfError = 0.15f;
    private Transform player;
    private bool reverse;
    protected override void SpecificStart()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected override void SpecificUpdate()
    {
        if (transform.position.x > player.position.x && reverse)
        {
            transform.Rotate(0, -180, 0);
            reverse = false;
        }
        else if (transform.position.x < player.position.x && reverse == false)
        {
            transform.Rotate(0, 180, 0);
            reverse = true;
        }
        if (moving == false)
        {
            moving = Mathf.Abs(transform.position.x - player.position.x) <= aggroRange.x && Mathf.Abs(transform.position.y - player.position.y) <= aggroRange.y;
        }
    }
    protected override void Move()
    {
        int x = transform.position.x > player.position.x ? -1 : 1;
        int y = transform.position.y > player.position.y ? -1 : 1;
        if (Mathf.Abs(transform.position.x - player.position.x) >= margainOfError)
        {
            rBody.position += new Vector2(x * speed, 0);
        }
        if (Mathf.Abs(transform.position.y - player.position.y) >= margainOfError)
        {
            rBody.position += new Vector2(0, y * speed);
        }
    }
}
