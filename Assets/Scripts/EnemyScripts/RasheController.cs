using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasheController : EnemyController
{
    private Transform player;
    private bool horiz, fired;
    [SerializeField]
    private GameObject missile;
    private float xSpeed;
    // Start is called before the first frame update
    protected override void SpecificStart()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        moving = true;
        xSpeed = speed;
    }

    // Update is called once per frame
    protected override void SpecificUpdate() { }

    protected override void Move()
    {
        if (horiz)
        {
            if (fired == false && ((speed > 0 && rBody.position.x >= player.position.x) || (speed < 0 && rBody.position.x <= player.position.x)))
            {
                Instantiate(missile, transform.position, transform.rotation);
                fired = true;
            }
            rBody.position += new Vector2(xSpeed, 0);
        }
        else
        {
            rBody.position += new Vector2(0, speed);
            horiz = rBody.position.y > player.position.y;
            xSpeed = rBody.position.x > player.position.y ? -speed : speed;
        }
    }
}
